import { Injectable } from "@angular/core";
import { AbstractClientApiService } from "src/app/lib/client-api-service/abstract-client-api.service";

@Injectable({
  providedIn: "root",
})
export class LoginService {
  constructor(private clientApiService: AbstractClientApiService) {}

  public async login(login: string, password: string): Promise<void> {
    return this.clientApiService.callClient("client_tryLogin", {
      login: login,
      password: password,
    });
  }
}
