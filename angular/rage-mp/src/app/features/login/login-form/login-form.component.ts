import { Component, OnInit } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { TranslocoService } from "@ngneat/transloco";
import { LoginService } from "../login.service";

@Component({
  selector: "app-login-form",
  templateUrl: "./login-form.component.html",
  styleUrls: ["./login-form.component.scss"],
})
export class LoginFormComponent implements OnInit {
  public disappear = false;
  public loginForm!: FormGroup;
  public loggingIn = false;

  constructor(
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
    if (this.loginForm.invalid) {
      return;
    }

    this.loggingIn = true;

    this.loginService
      .login(
        this.loginForm.get("login")?.value,
        this.loginForm.get("password")?.value,
      )
      .then((val) => {
        this.loggingIn = false;
      });
  }
}
