import { ApiResponse } from "./client-api.model";

export abstract class AbstractClientApiService {
  abstract callClient(eventName: string, data?: object): Promise<ApiResponse>;
  abstract registerEvent(
    eventName: string,
    callBack: (args: any, info: any) => void,
  ): void;
}
