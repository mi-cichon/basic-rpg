import { Component, OnInit } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { TranslocoService } from "@ngneat/transloco";
import { LoginService } from "../login.service";
import { NotificationService } from "../../notifications/notification/notification.service";

@Component({
  selector: "app-login-form",
  templateUrl: "./login-form.component.html",
  styleUrls: ["./login-form.component.scss"],
})
export class LoginFormComponent implements OnInit {
  public disappear = false;
  public loginForm!: FormGroup;

  constructor(
    private notificationService: NotificationService,
    private loginService: LoginService,
    private translocoService: TranslocoService,
    private router: Router,
  ) {}

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      login: new FormControl("", Validators.required),
      password: new FormControl("", [Validators.required]),
    });
  }

  public goToRegister() {
    this.disappear = true;
    setTimeout(() => {
      this.router.navigateByUrl("/login/register");
    }, 400);
  }

  public changeLocale(locale: string) {
    this.translocoService.setActiveLang(locale);
  }

  public submit(): void {
    if (this.loginForm.invalid || this.disappear) {
      return;
    }

    this.loginService
      .login(
        this.loginForm.get("login")?.value,
        this.loginForm.get("password")?.value,
      )
      .then((response) => {
        if (response.responseType === "success") {
          if (response.data) {
            this.loginService.lastPosAvailable = response.data[
              "hasLastPos" as keyof object
            ] as boolean;
          }
          this.router.navigateByUrl("/login/spawn-selection");
        } else {
          this.notificationService.showNotification(
            response.message,
            response.responseType,
          );
        }
      });
  }
}
