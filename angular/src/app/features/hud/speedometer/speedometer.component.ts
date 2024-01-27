import { Component } from "@angular/core";
import { AbstractClientApiService } from "src/app/lib/client-api-service/abstract-client-api.service";
import {
  SpeedometerData,
  mapObjectToSpeedometerData,
} from "./speedometer.model";

@Component({
  selector: "app-speedometer",
  templateUrl: "./speedometer.component.html",
  styleUrls: ["./speedometer.component.scss"],
})
export class SpeedometerComponent {
  public speedometerData?: SpeedometerData;
  // {
  //   speed: 140,
  //   rpm: 0.8,
  //   petrol: 0.5,
  //   damagedEngine: true,
  //   damagedSuspension: false,
  //   trip: 0,
  // };

  constructor(clientApiService: AbstractClientApiService) {
    clientApiService
      .registerEvent("browser_updateSpeedometer")
      .subscribe((response) => {
        if (!response.data || response.responseType != "success") {
          return;
        }

        this.speedometerData = mapObjectToSpeedometerData(response.data);
      });
  }

  private addLeadingZeros(num: number): string {
    const numWithZeros = "000000" + num.toString();
    return numWithZeros.substring(numWithZeros.length - 6);
  }
}
