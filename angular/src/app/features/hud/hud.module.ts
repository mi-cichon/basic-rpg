import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { HudRoutingModule } from "./hud-routing.module";
import { TranslocoModule } from "@ngneat/transloco";
import { MainHudComponent } from "./main-hud/main-hud.component";
import { PlayerInfoComponent } from "./player-info/player-info.component";
import { ChatComponent } from "./chat/chat.component";
import { SpeedometerComponent } from "./speedometer/speedometer.component";
import { ChatInputComponent } from "./chat/chat-input/chat-input.component";
import { InfoMessageComponent } from "./chat/messages/info-message/info-message.component";
import { PrivateMessageComponent } from "./chat/messages/private-message/private-message.component";
import { RegularMessageComponent } from "./chat/messages/regular-message/regular-message.component";
import { GlobalMessageComponent } from "./chat/messages/global-message/global-message.component";
import { PenaltyMessageComponent } from "./chat/messages/penalty-message/penalty-message.component";

@NgModule({
  declarations: [
    MainHudComponent,
    PlayerInfoComponent,
    ChatComponent,
    SpeedometerComponent,
    ChatInputComponent,
    InfoMessageComponent,
    PrivateMessageComponent,
    RegularMessageComponent,
    GlobalMessageComponent,
    PenaltyMessageComponent,
  ],
  imports: [CommonModule, HudRoutingModule, TranslocoModule],
})
export class HudModule {}
