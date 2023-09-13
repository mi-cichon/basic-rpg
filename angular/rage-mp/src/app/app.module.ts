import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { AbstractClientApiService } from "./lib/client-api-service/abstract-client-api.service";
import { TranslocoRootModule } from "./core/i18n/transloco/transloco-root.module";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { NotificationComponent } from "./features/notifications/notification/notification.component";
import { ClientApiMockService } from "./lib/client-api-service/client-api-mock.service";
import { HashLocationStrategy, LocationStrategy } from "@angular/common";
import { ClientApiProdService } from "./lib/client-api-service/client-api-prod.service";

@NgModule({
  declarations: [AppComponent, NotificationComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    TranslocoRootModule,
    BrowserAnimationsModule,
  ],
  providers: [
    { provide: LocationStrategy, useClass: HashLocationStrategy },
    // { provide: AbstractClientApiService, useClass: ClientApiMockService },
    { provide: AbstractClientApiService, useClass: ClientApiProdService },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
