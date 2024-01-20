import { AfterViewInit, Component, EventEmitter, Output } from "@angular/core";
import { AbstractClientApiService } from "src/app/lib/client-api-service/abstract-client-api.service";
import { ChatInputService } from "./chat-input.service";

@Component({
  selector: "app-chat-input",
  templateUrl: "./chat-input.component.html",
  styleUrls: ["./chat-input.component.scss"],
})
export class ChatInputComponent implements AfterViewInit {
  private currentMessageIndex = -1;
  @Output() closeInput = new EventEmitter<void>();

  constructor(
    private clientApiService: AbstractClientApiService,
    private chatInputService: ChatInputService,
  ) {}

  ngAfterViewInit() {
    document.getElementById("input")?.focus();
  }

  public sendMessage(event: any): void {
    const message: string = event.target.value;
    if (message.length === 0) {
      return;
    }
    this.chatInputService.sentMessages.unshift(message);
    this.currentMessageIndex = -1;
    event.target.value = "";
    this.clientApiService.pokeClient("client_sendMessage", message);
    this.closeInput.emit();
  }

  public previousMessage(event: any): void {
    if (
      this.chatInputService.sentMessages.length >
      this.currentMessageIndex + 1
    ) {
      this.currentMessageIndex += 1;
    }
    event.target.value =
      this.chatInputService.sentMessages[this.currentMessageIndex];
  }

  public nextMessage(event: any): void {
    if (this.currentMessageIndex > 0) {
      this.currentMessageIndex -= 1;
      event.target.value =
        this.chatInputService.sentMessages[this.currentMessageIndex];
    } else if (this.currentMessageIndex === 0) {
      this.currentMessageIndex = -1;
      event.target.value = "";
    }
  }
}
