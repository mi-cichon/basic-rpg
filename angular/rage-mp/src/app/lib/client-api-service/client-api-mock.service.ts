import { AbstractClientApiService } from "./abstract-client-api.service";

export class ClientApiMockService extends AbstractClientApiService {
  override async callClient(
    eventName: string,
    data?: object | undefined,
  ): Promise<any> {
    console.log(
      `Called client event ${eventName} with data: ${JSON.stringify(data)}.`,
    );
    return new Promise((resolve) => setTimeout(resolve, 2000));
  }
  override registerEvent(
    eventName: string,
    callBack: (args: any, info: any) => void,
  ): void {
    console.log(`Registered browser event ${eventName}.`);
  }
}
