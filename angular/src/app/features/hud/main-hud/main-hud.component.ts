import { Component } from "@angular/core";
import { AbstractClientApiService } from "src/app/lib/client-api-service/abstract-client-api.service";

@Component({
  selector: "app-main-hud",
  templateUrl: "./main-hud.component.html",
  styleUrls: ["./main-hud.component.scss"],
})
export class MainHudComponent {
  public showSpeedometer = false;
  public showSpeedometerControls = false;

  constructor(clientApiService: AbstractClientApiService) {
    clientApiService
      .registerEvent("browser_displaySpeedometer")
      .subscribe((response) => {
        if (!response.data || response.responseType != "success") {
          return;
        }

        const state = response.data["state" as keyof object] as boolean;
        this.showSpeedometer = state;
        this.showSpeedometerControls = false;
      });

    clientApiService
      .registerEvent("browser_switchSpeedometerControls")
      .subscribe(() => {
        this.showSpeedometerControls = !this.showSpeedometerControls;
      });
  }
}
