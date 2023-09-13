import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { MainHudComponent } from "./main-hud/main-hud.component";

const routes: Routes = [
  {
    path: "",
    component: MainHudComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class HudRoutingModule {}
