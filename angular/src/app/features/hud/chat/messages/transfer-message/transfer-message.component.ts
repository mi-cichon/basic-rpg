import { Input, Component } from "@angular/core";
@Component({
  selector: "app-transfer-message",
  templateUrl: "./transfer-message.component.html",
  styleUrls: [
    "./transfer-message.component.scss",
    "./../base-message-style.scss",
  ],
})
export class TransferMessageComponent {
  @Input({ required: true }) messageText!: string;
  @Input({ required: true }) playerName!: string;
  @Input({ required: true }) playerId!: number;
  @Input({ required: true }) from!: boolean;
  @Input({ required: true }) value!: string;
}
