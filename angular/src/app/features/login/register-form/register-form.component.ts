import { Component, OnInit } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { confirmPasswordValidator } from "../../shared/validators/common-validators";
import { LoginService } from "../login.service";
import { NotificationService } from "../../notifications/notification/notification.service";

@Component({
  selector: "app-register-form",
  templateUrl: "./register-form.component.html",
  styleUrls: ["./register-form.component.scss"],
})
export class RegisterFormComponent implements OnInit {
  public registerForm!: FormGroup;
  public disappear = false;
  constructor(
    private router: Router,
    private loginService: LoginService,
    private notificationService: NotificationService,
  ) {}

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
    if (this.disappear || this.registerForm.invalid) {
      return;
    }

    this.loginService
      .register(
        this.registerForm.get("login")?.value,
        this.registerForm.get("password")?.value,
      )
      .then((response) => {
        if (response.responseType === "success") {
          this.goToLogin();
          this.notificationService.showNotification(
            "register.successful",
            "success",
          );
        } else {
          this.notificationService.showNotification(
            response.message,
            response.responseType,
          );
        }
      });
  }
}
