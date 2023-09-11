import * as rpc from "rage-rpc";
import { AbstractClientApiService } from "./abstract-client-api.service";

export class ClientApiProdService extends AbstractClientApiService {
  public async callClient(eventName: string, data?: object): Promise<any> {
    return new Promise((resolve, reject) => {
      rpc.register(`response_${eventName}`, (args) => {
        resolve(args);
      });
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
