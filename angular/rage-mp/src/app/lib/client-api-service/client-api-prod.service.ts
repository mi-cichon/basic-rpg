import * as rpc from "rage-rpc";
import { AbstractClientApiService } from "./abstract-client-api.service";
import { SpinnerService } from "src/app/features/shared/components/spinner/spinner.service";
import { Inject, Injectable } from "@angular/core";
import {
  ApiResponse,
  mapApiResponseTypeToResponseType,
} from "./client-api.model";

@Injectable()
export class ClientApiProdService extends AbstractClientApiService {
  constructor(@Inject(SpinnerService) private spinnerService: SpinnerService) {
    super();
  }
  public async callClient(
    eventName: string,
    data?: object,
  ): Promise<ApiResponse> {
    return new Promise((resolve, reject) => {
      rpc.register(`response_${eventName}`, (response) => {
        this.spinnerService.processingApiCall = false;
        const mappedResponse = {
          message: response.Message as string,
          data: response.Data as object,
          responseType: mapApiResponseTypeToResponseType(response.ResponseType),
        } as ApiResponse;
        resolve(mappedResponse);
      });
      this.spinnerService.processingApiCall = true;
      rpc.callClient(eventName, data);
    });
  }

  public registerEvent(
    eventName: string,
    callBack: (args: any, info: any) => void,
  ): void {
    rpc.register(eventName, callBack);
  }
}
