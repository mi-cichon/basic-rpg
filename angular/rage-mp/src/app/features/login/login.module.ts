import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { LoginRoutingModule } from "./login-routing.module";
import { LoginFormComponent } from "./login-form/login-form.component";
import { TranslocoModule } from "@ngneat/transloco";
import { HttpClientModule } from "@angular/common/http";
import { RegisterFormComponent } from "./register-form/register-form.component";
import { SpinnerComponent } from "../shared/components/spinner/spinner.component";
import { ReactiveFormsModule } from "@angular/forms";
import { FormButtonComponent } from "../shared/components/form-button/form-button.component";
import { InputComponent } from "../shared/components/input/input.component";

@NgModule({
  declarations: [LoginFormComponent, RegisterFormComponent],
  imports: [
    CommonModule,
    LoginRoutingModule,
    TranslocoModule,
    HttpClientModule,
    SpinnerComponent,
    ReactiveFormsModule,
    FormButtonComponent,
    InputComponent,
  ],
})
export class LoginModule {}
