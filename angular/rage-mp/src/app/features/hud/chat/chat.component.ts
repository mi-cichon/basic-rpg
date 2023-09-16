import {
  Component,
  ElementRef,
  OnDestroy,
  OnInit,
  ViewChild,
} from "@angular/core";
import { ChatMessage, mapObjectToMessage } from "./chat.model";
import { BehaviorSubject, Subscription } from "rxjs";
import { AbstractClientApiService } from "src/app/lib/client-api-service/abstract-client-api.service";
import { ChatInputComponent } from "./chat-input/chat-input.component";

@Component({
  selector: "app-chat",
  templateUrl: "./chat.component.html",
  styleUrls: ["./chat.component.scss"],
})
export class ChatComponent implements OnInit, OnDestroy {
  @ViewChild("chat-scroll") chatScroll!: ElementRef;
  @ViewChild("#chatInput") chatInput!: ChatInputComponent;
  public showInput = false;
  public messages: ChatMessage[] = [];

  public newMessageSub!: Subscription;
  public showInputSub!: Subscription;

  constructor(private clientApiService: AbstractClientApiService) {}

  ngOnDestroy(): void {
    this.newMessageSub.unsubscribe();
    this.showInputSub.unsubscribe();
  }

  ngOnInit(): void {
    this.newMessageSub = this.clientApiService
      .registerEvent("browser_newMessage")
      .subscribe((response) => {
        if (response.data) {
          this.messages.push(mapObjectToMessage(response.data));
        }
      });

    this.showInputSub = this.clientApiService
      .registerEvent("browser_showChatInput")
      .subscribe((response) => {
        if (response.data) {
          const data: any = response.data;
          const show = data.showInput as boolean;
          this.showInput = show;
        }
      });
  }

  public closeInput(): void {
    this.showInput = false;
  }

  public scrollToBottom(): void {
    const chatScroll = document.getElementById("chat-scroll");
    if (chatScroll === null) {
      return;
    }
    chatScroll.scrollTop = chatScroll.scrollHeight;
  }
}
