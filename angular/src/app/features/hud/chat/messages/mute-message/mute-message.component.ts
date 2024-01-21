import { Component, Input } from "@angular/core";

@Component({
  selector: "app-mute-message",
  templateUrl: "./mute-message.component.html",
  styleUrls: ["./mute-message.component.scss", "./../base-message-style.scss"],
})
export class MuteMessageComponent {
  @Input({ required: true }) messageText!: string;
  @Input({ required: true }) playerName!: string;
  @Input({ required: true }) punishmentLength!: number;
  @Input({ required: true }) additionalName!: string;
}
