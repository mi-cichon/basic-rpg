import { CommonModule } from "@angular/common";
import { Component, Input } from "@angular/core";

@Component({
  selector: "app-progress-bar",
  standalone: true,
  imports: [CommonModule],
  templateUrl: "./progress-bar.component.html",
  styleUrls: ["./progress-bar.component.scss"],
})
export class ProgressBarComponent {
  @Input({ required: true }) value!: number;
  @Input() color?: string = "white";
}
