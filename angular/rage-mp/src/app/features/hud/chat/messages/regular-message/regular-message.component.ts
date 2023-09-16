import { Component, Input } from "@angular/core";

@Component({
  selector: "app-regular-message",
  templateUrl: "./regular-message.component.html",
  styleUrls: [
    "./regular-message.component.scss",
    "./../base-message-style.scss",
  ],
})
export class RegularMessageComponent {
  @Input({ required: true }) messageText!: string;
  @Input({ required: true }) playerName!: string;
  @Input({ required: true }) playerId!: number;
}
