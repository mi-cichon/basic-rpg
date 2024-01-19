import { Component, HostBinding, Input } from "@angular/core";
import { ThemePalette } from "@angular/material/core";
import {
  MatProgressSpinnerModule,
  ProgressSpinnerMode,
} from "@angular/material/progress-spinner";
import { SpinnerService } from "./spinner.service";

@Component({
  selector: "app-spinner",
  templateUrl: "./spinner.component.html",
  styleUrls: ["./spinner.component.scss"],
  standalone: true,
  imports: [MatProgressSpinnerModule],
})
export class SpinnerComponent {
  @Input() textKey?: string;
  @Input() spinnerType: SpinnerType = "manual";
  @HostBinding("class.hide-spinner-host") get hideHost() {
    return (
      this.spinnerType === "manual" ||
      (this.spinnerType === "apiCall" && this.spinnerService.processingApiCall)
    );
  }

  constructor(public spinnerService: SpinnerService) {}

  color: ThemePalette = "warn";
  mode: ProgressSpinnerMode = "indeterminate";
  value = 50;
}

export type SpinnerType = "apiCall" | "manual";
