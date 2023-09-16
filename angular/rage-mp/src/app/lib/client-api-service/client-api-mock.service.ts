import { SpinnerService } from "src/app/features/shared/components/spinner/spinner.service";
import { AbstractClientApiService } from "./abstract-client-api.service";
import { Inject, Injectable } from "@angular/core";
import { ApiResponse } from "./client-api.model";
import { Observable, Subscriber } from "rxjs";

@Injectable()
export class ClientApiMockService extends AbstractClientApiService {
  constructor(@Inject(SpinnerService) private spinnerService: SpinnerService) {
    super();
  }
  override async callClient(
    eventName: string,
    data?: object | undefined,
  ): Promise<ApiResponse> {
    console.log(
      `Called client event ${eventName} with data: ${JSON.stringify(data)}.`,
    );
    this.spinnerService.processingApiCall = true;
    return new Promise((resolve) => {
      setTimeout(() => {
        this.spinnerService.processingApiCall = false;
        resolve({
          responseType: "success",
          message: "success",
          data: {},
        });
      }, 2000);
    });
  }
  override registerEvent(eventName: string): Observable<ApiResponse> {
    const response$ = new Observable((observer: Subscriber<ApiResponse>) => {
      console.log(`Registered client event ${eventName}.`);
      setTimeout(() => {
        observer.next({
          responseType: "success",
          message: "success",
          data: {},
        });
      }, 2000);
    });
    return response$;
  }

  override pokeClient(eventName: string, data?: object): void {
    console.log(`Poked client event ${eventName}.`);
  }
}
