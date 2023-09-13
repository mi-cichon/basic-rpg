import { Injectable } from "@angular/core";
import { AbstractClientApiService } from "src/app/lib/client-api-service/abstract-client-api.service";
import { ApiResponse } from "src/app/lib/client-api-service/client-api.model";

@Injectable({
  providedIn: "root",
})
export class LoginService {
  public lastPosAvailable = false;

  constructor(private clientApiService: AbstractClientApiService) {}

  public async login(login: string, password: string): Promise<ApiResponse> {
    return this.clientApiService.callClient("client_tryLogin", {
      login: login,
      password: password,
    });
  }

  public async register(login: string, password: string): Promise<ApiResponse> {
    return this.clientApiService.callClient("client_tryRegister", {
      login: login,
      password: password,
    });
  }

  public async selectSpawn(spawnId: number): Promise<ApiResponse> {
    return this.clientApiService.callClient("client_spawnSelected", {
      spawnId: spawnId,
    });
  }
}
