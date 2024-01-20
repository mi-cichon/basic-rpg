import { Component, Input } from "@angular/core";

@Component({
  selector: "app-info-message",
  templateUrl: "./info-message.component.html",
  styleUrls: ["./info-message.component.scss", "./../base-message-style.scss"],
})
export class InfoMessageComponent {
  @Input({ required: true }) messageText!: string;
}
