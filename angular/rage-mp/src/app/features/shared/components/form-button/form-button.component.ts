import { Component, Host } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormGroupDirective, ReactiveFormsModule } from "@angular/forms";

@Component({
  selector: "app-form-button",
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: "./form-button.component.html",
  styleUrls: ["./form-button.component.scss"],
})
export class FormButtonComponent {
  constructor(@Host() public formGroup: FormGroupDirective) {}

  public touchControls(): void {
    this.formGroup.control.markAllAsTouched();
  }
}
