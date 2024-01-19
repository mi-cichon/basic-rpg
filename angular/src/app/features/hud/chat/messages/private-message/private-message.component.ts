import { Component, Input } from "@angular/core";

@Component({
  selector: "app-private-message",
  templateUrl: "./private-message.component.html",
  styleUrls: [
    "./private-message.component.scss",
    "./../base-message-style.scss",
  ],
})
export class PrivateMessageComponent {
  @Input({ required: true }) messageText!: string;
  @Input({ required: true }) playerName!: string;
  @Input({ required: true }) playerId!: number;
  @Input({ required: true }) from!: boolean;
}
