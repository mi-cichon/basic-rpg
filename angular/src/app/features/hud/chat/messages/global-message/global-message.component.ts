import { Component, Input } from "@angular/core";

@Component({
  selector: "app-global-message",
  templateUrl: "./global-message.component.html",
  styleUrls: [
    "./global-message.component.scss",
    "./../base-message-style.scss",
  ],
})
export class GlobalMessageComponent {
  @Input({ required: true }) messageText!: string;
  @Input({ required: true }) playerName!: string;
  @Input({ required: true }) playerId!: number;
}
