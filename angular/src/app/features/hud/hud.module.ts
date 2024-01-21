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
import { TranslateIfKeyExistsPipe } from "./chat/pipes/translate-if-key-exists.pipe";
import { TransferMessageComponent } from "./chat/messages/transfer-message/transfer-message.component";
import { GroupMessageComponent } from "./chat/messages/group-message/group-message.component";
import { MuteMessageComponent } from "./chat/messages/mute-message/mute-message.component";
import { LicenceMessageComponent } from "./chat/messages/licence-message/licence-message.component";
import { KickMessageComponent } from "./chat/messages/kick-message/kick-message.component";
import { BanMessageComponent } from "./chat/messages/ban-message/ban-message.component";

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
    TransferMessageComponent,
    GroupMessageComponent,
    MuteMessageComponent,
    LicenceMessageComponent,
    KickMessageComponent,
    BanMessageComponent,
  ],
  imports: [
    CommonModule,
    HudRoutingModule,
    TranslocoModule,
    TranslateIfKeyExistsPipe,
  ],
})
export class HudModule {}
