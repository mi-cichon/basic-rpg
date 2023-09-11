export abstract class AbstractClientApiService {
  abstract callClient(eventName: string, data?: object): Promise<any>;
  abstract registerEvent(
    eventName: string,
    callBack: (args: any, info: any) => void,
  ): void;
}
