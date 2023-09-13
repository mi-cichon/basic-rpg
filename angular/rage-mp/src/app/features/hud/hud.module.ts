import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { HudRoutingModule } from "./hud-routing.module";
import { TranslocoModule } from "@ngneat/transloco";
import { MainHudComponent } from "./main-hud/main-hud.component";

@NgModule({
  declarations: [MainHudComponent],
  imports: [CommonModule, HudRoutingModule, TranslocoModule],
})
export class HudModule {}
