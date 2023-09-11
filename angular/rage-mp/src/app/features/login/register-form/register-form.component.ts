import { Component, OnInit } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { confirmPasswordValidator } from "../../shared/validators/common-validators";

@Component({
  selector: "app-register-form",
  templateUrl: "./register-form.component.html",
  styleUrls: ["./register-form.component.scss"],
})
export class RegisterFormComponent implements OnInit {
  public registerForm!: FormGroup;
  public disappear = false;
  public registering = false;
  constructor(private router: Router) {}

  public goToLogin(): void {
    this.disappear = true;
    setTimeout(() => {
      this.router.navigateByUrl("/login");
    }, 400);
  }

  ngOnInit(): void {
    this.registerForm = new FormGroup({
      login: new FormControl("", Validators.required),
      password: new FormControl("", [Validators.required]),
      confirmPassword: new FormControl("", [
        Validators.required,
        confirmPasswordValidator("password"),
      ]),
    });
  }

  public register(): void {
    if (this.disappear) {
      return;
    }

    this.registering = true;
    setTimeout(() => {
      this.registering = false;
    }, 1000);
  }
}
