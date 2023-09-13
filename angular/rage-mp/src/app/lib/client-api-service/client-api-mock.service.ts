import { SpinnerService } from "src/app/features/shared/components/spinner/spinner.service";
import { AbstractClientApiService } from "./abstract-client-api.service";
import { Inject, Injectable } from "@angular/core";
import { ApiResponse } from "./client-api.model";

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
  override registerEvent(
    eventName: string,
    callBack: (args: any, info: any) => void,
  ): void {
    console.log(`Registered browser event ${eventName}.`);
  }
}
