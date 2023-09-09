import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { ClientApiService } from 'src/app/lib/client-api.service';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private _showLogin = new BehaviorSubject<boolean>(false);
  public get showLogin$(): BehaviorSubject<boolean>{
    return this._showLogin;
  }
  
  constructor(rpcService: ClientApiService) {
   }

   private handleShowLoginForm(state: boolean): void {
    this._showLogin.next(true);
   }
}
