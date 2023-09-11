import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { ClientApiMockService } from "./lib/client-api-service/client-api-mock.service";
import { AbstractClientApiService } from "./lib/client-api-service/abstract-client-api.service";
import { TranslocoRootModule } from "./core/i18n/transloco/transloco-root.module";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { ClientApiProdService } from "./lib/client-api-service/client-api-prod.service";

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    TranslocoRootModule,
    BrowserAnimationsModule,
  ],
  providers: [
    //{ provide: AbstractClientApiService, useClass: ClientApiMockService },
    { provide: AbstractClientApiService, useClass: ClientApiProdService },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
