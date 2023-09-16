import * as rpc from "rage-rpc";
import { AbstractClientApiService } from "./abstract-client-api.service";
import { SpinnerService } from "src/app/features/shared/components/spinner/spinner.service";
import { Inject, Injectable, NgZone } from "@angular/core";
import {
  ApiResponse,
  mapApiResponseTypeToResponseType,
} from "./client-api.model";
import { Observable, Subscriber } from "rxjs";

@Injectable()
export class ClientApiProdService extends AbstractClientApiService {
  constructor(
    @Inject(SpinnerService) private spinnerService: SpinnerService,
    @Inject(NgZone) private ngZone: NgZone,
  ) {
    super();
  }
  public async callClient(
    eventName: string,
    data?: unknown,
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

  public registerEvent(eventName: string): Observable<ApiResponse> {
    const response$ = new Observable((observer: Subscriber<ApiResponse>) => {
      rpc.register(`${eventName}`, (response) => {
        this.ngZone.run(() => {
          const mappedResponse = {
            message: response.Message as string,
            data: response.Data as object,
            responseType: mapApiResponseTypeToResponseType(
              response.ResponseType,
            ),
          } as ApiResponse;
          observer.next(mappedResponse);
        });
      });
    });
    return response$;
  }

  public pokeClient(eventName: string, data?: unknown): void {
    rpc.callClient(`${eventName}`, data);
  }
}
