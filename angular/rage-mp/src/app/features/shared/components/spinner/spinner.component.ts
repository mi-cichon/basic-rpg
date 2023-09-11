import { Component, Input } from "@angular/core";
import { ThemePalette } from "@angular/material/core";
import {
  MatProgressSpinnerModule,
  ProgressSpinnerMode,
} from "@angular/material/progress-spinner";
import { TranslocoModule } from "@ngneat/transloco";

@Component({
  selector: "app-spinner",
  templateUrl: "./spinner.component.html",
  styleUrls: ["./spinner.component.scss"],
  standalone: true,
  imports: [MatProgressSpinnerModule, TranslocoModule],
})
export class SpinnerComponent {
  @Input() textKey?: string;

  color: ThemePalette = "warn";
  mode: ProgressSpinnerMode = "indeterminate";
  value = 50;
}
