import { CommonModule } from "@angular/common";
import { Component, Input } from "@angular/core";

@Component({
  selector: "app-speedometer-control",
  standalone: true,
  imports: [CommonModule],
  templateUrl: "./speedometer-control.component.html",
  styleUrls: ["./speedometer-control.component.scss"],
})
export class SpeedometerControlComponent {
  @Input({ required: true }) iconFileName!: string;
  @Input({ required: true }) active!: boolean;
  @Input() activeColor: "green" | "red" = "green";
  public readonly iconBaseUrl = "/assets/images/speedometer/icons/";
}
