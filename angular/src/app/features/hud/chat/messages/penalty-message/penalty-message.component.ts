import { Component, Input } from "@angular/core";

@Component({
  selector: "app-penalty-message",
  templateUrl: "./penalty-message.component.html",
  styleUrls: [
    "./penalty-message.component.scss",
    "./../base-message-style.scss",
  ],
})
export class PenaltyMessageComponent {
  @Input({ required: true }) messageText!: string;
}
