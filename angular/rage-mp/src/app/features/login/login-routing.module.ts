import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { LoginFormComponent } from "./login-form/login-form.component";
import { RegisterFormComponent } from "./register-form/register-form.component";
import { SpawnSelectionComponent } from "./spawn-selection/spawn-selection/spawn-selection.component";

const routes: Routes = [
  {
    path: "",
    component: LoginFormComponent,
  },
  {
    path: "register",
    component: RegisterFormComponent,
  },
  {
    path: "spawn-selection",
    component: SpawnSelectionComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class LoginRoutingModule {}
