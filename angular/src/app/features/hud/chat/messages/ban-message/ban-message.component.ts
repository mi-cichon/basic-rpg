import { Component, Input } from "@angular/core";

@Component({
  selector: "app-ban-message",
  templateUrl: "./ban-message.component.html",
  styleUrls: ["./ban-message.component.scss", "./../base-message-style.scss"],
})
export class BanMessageComponent {
  @Input({ required: true }) messageText!: string;
  @Input({ required: true }) playerName!: string;
  @Input({ required: true }) punishmentLength!: number;
  @Input({ required: true }) additionalName!: string;
}
