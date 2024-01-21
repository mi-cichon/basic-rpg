import { Component, Input } from "@angular/core";

@Component({
  selector: "app-kick-message",
  templateUrl: "./kick-message.component.html",
  styleUrls: ["./kick-message.component.scss", "./../base-message-style.scss"],
})
export class KickMessageComponent {
  @Input({ required: true }) messageText!: string;
  @Input({ required: true }) playerName!: string;
  @Input({ required: true }) additionalName!: string;
}
