import { Injectable } from '@angular/core';
import * as rpc from 'rage-rpc';

@Injectable({
  providedIn: 'root'
})
export class ClientApiService {

  constructor() { }

  public callClient(funcName: string, data?: object): void {
    rpc.callClient(funcName, data);
  }

  public registerEvenet(eventName: string, callBack: rpc.ProcedureListener): void {
    rpc.register(eventName, callBack);
  }
}
