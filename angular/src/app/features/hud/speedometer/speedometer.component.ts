import { AfterViewInit, Component, ElementRef, ViewChild } from "@angular/core";
import { AbstractClientApiService } from "src/app/lib/client-api-service/abstract-client-api.service";
import {
  SpeedometerData,
  getPetrolStep,
  mapObjectToSpeedometerData,
} from "./speedometer.model";

@Component({
  selector: "app-speedometer",
  templateUrl: "./speedometer.component.html",
  styleUrls: ["./speedometer.component.scss"],
})
export class SpeedometerComponent {
  private readonly petrolMaxValue = 0.55;
  private readonly rpmDegrees = 190;
  private readonly stepUrl = "/assets/images/speedometer/petrol-steps/";
  private readonly stepUrlExtension = ".png";

  @ViewChild("rpmElement") rpmElement!: ElementRef;
  @ViewChild("speedElement") speedElement!: ElementRef;
  @ViewChild("tripKilometerElement") tripKilometerElement!: ElementRef;
  @ViewChild("tripMeterElement") tripMeterElement!: ElementRef;
  @ViewChild("petrolBodyElement") petrolBodyElement!: ElementRef;

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

  private setSpeedometerValues(data: SpeedometerData) {
    this.speedElement.nativeElement.innerHTML = data.speed.toString();
    this.rpmElement.nativeElement.style = `transform: translate(-50%, -50%) rotate(${
      data.rpm * this.rpmDegrees - 5
    }deg);`;

    const petrolStep = getPetrolStep(data.petrol);
    this.petrolBodyElement.nativeElement.style = `background-image: url('${this.stepUrl}${petrolStep}${this.stepUrlExtension}');`;

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
