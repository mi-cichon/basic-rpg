import { Component, Input } from "@angular/core";

@Component({
  selector: "app-group-message",
  templateUrl: "./group-message.component.html",
  styleUrls: ["./group-message.component.scss", "./../base-message-style.scss"],
})
export class GroupMessageComponent {
  @Input({ required: true }) messageText!: string;
  @Input({ required: true }) playerName!: string;
  @Input({ required: true }) playerId!: number;
  @Input({ required: true }) organisation!: string;
}
