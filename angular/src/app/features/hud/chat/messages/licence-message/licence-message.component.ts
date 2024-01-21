import { Component, Input } from "@angular/core";

@Component({
  selector: "app-licence-message",
  templateUrl: "./licence-message.component.html",
  styleUrls: [
    "./licence-message.component.scss",
    "./../base-message-style.scss",
  ],
})
export class LicenceMessageComponent {
  @Input({ required: true }) messageText!: string;
  @Input({ required: true }) playerName!: string;
  @Input({ required: true }) punishmentLength!: number;
  @Input({ required: true }) additionalName!: string;
}
