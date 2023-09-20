import { AfterViewInit, Component, ElementRef, ViewChild } from "@angular/core";
import * as ProgressBar from "progressbar.js";
import Shape from "progressbar.js/shape";
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
export class SpeedometerComponent implements AfterViewInit {
  private petrolProgressBar!: Shape;
  private readonly petrolMaxValue = 0.55;
  private readonly rpmDegrees = 190;

  @ViewChild("rpmElement") rpmElement!: ElementRef;
  @ViewChild("speedElement") speedElement!: ElementRef;
  @ViewChild("tripKilometerElement") tripKilometerElement!: ElementRef;
  @ViewChild("tripMeterElement") tripMeterElement!: ElementRef;

  constructor(clientApiService: AbstractClientApiService) {
    clientApiService
      .registerEvent("browser_updateSpeedometer")
      .subscribe((response) => {
        if (!response.data || response.responseType != "success") {
          return;
        }

        const data = mapObjectToSpeedometerData(response.data);
        this.setSpeedometerValues(data);
      });
  }

  ngAfterViewInit(): void {
    const petrolBackProgress = new ProgressBar.Circle(".petrol-back", {
      strokeWidth: 12,
      easing: "easeInOut",
      duration: 10,
      color: "#19233F",
      trailColor: "rgba(255,0,0,0)",
      trailWidth: 2,
      svgStyle: null,
    });
    this.petrolProgressBar = new ProgressBar.Circle(".petrol", {
      strokeWidth: 6,
      easing: "easeInOut",
      duration: 10,
      color: "#fff",
      trailColor: "rgba(255,0,0,0)",
      trailWidth: 1,
      svgStyle: null,
    });

    this.petrolProgressBar.set(0);
    petrolBackProgress.set(this.petrolMaxValue);
  }

  private setSpeedometerValues(data: SpeedometerData) {
    this.speedElement.nativeElement.innerHTML = data.speed.toString();
    this.petrolProgressBar.set(data.petrol * this.petrolMaxValue);
    this.rpmElement.nativeElement.style = `transform: translate(-50%) rotate(${
      data.rpm * this.rpmDegrees - 5
    }deg);`;

    const roundedTrip = parseFloat(data.trip.toFixed(1));
    const decimalTrip = Math.floor(roundedTrip);
    const meters = ((roundedTrip - decimalTrip) * 10).toFixed(0);

    this.tripKilometerElement.nativeElement.innerHTML =
      this.addLeadingZeros(decimalTrip);
    this.tripMeterElement.nativeElement.innerHTML = meters;
  }

  private addLeadingZeros(num: number): string {
    const numWithZeros = "000000" + num.toString();
    return numWithZeros.substring(numWithZeros.length - 6);
  }
}
