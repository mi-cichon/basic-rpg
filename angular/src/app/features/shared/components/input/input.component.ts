import { Component, Host, Input, OnInit } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormGroupDirective, ReactiveFormsModule } from "@angular/forms";
import { Subscription } from "rxjs";

@Component({
  selector: "app-input",
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: "./input.component.html",
  styleUrls: ["./input.component.scss"],
})
export class InputComponent {
  @Input({ required: true }) inputType!: InputType;
  @Input({ required: true }) controlName!: string;
  @Input() inputPlaceholder?: string;
  @Input() icon?: string;

  private submitSubscription!: Subscription;

  constructor(@Host() public formGroup: FormGroupDirective) {}
}

type InputType =
  | "color"
  | "date"
  | "datetime-local"
  | "email"
  | "month"
  | "number"
  | "password"
  | "search"
  | "tel"
  | "text"
  | "time"
  | "url"
  | "week";
