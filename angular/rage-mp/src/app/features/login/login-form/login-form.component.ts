import { Component } from '@angular/core';
import { ClientApiService } from 'src/app/lib/client-api.service';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.scss']
})
export class LoginFormComponent {
  public showLogin: boolean = true;
  constructor(private clientApi: ClientApiService){
  }

  public login(): void {
    this.clientApi.registerEvenet('siema', () => {
      this.showLogin = false;
    })
    this.clientApi.callClient('siemazrana');
  }
}
