import {
  provideTransloco,
  Translation,
  TRANSLOCO_LOADER,
  TranslocoLoader,
  TranslocoModule,
} from "@ngneat/transloco";
import { Injectable, isDevMode, NgModule } from "@angular/core";
import { HttpClient, HttpClientModule } from "@angular/common/http";

@Injectable({ providedIn: "root" })
export class TranslocoHttpLoader implements TranslocoLoader {
  constructor(private http: HttpClient) {}

  getTranslation(lang: string) {
    return this.http.get<Translation>(`/assets/i18n/${lang}.json`);
  }
}

@NgModule({
  exports: [TranslocoModule],
  imports: [HttpClientModule],
  providers: [
    provideTransloco({
      config: {
        availableLangs: ["pl", "en"],
        defaultLang: "pl",
        reRenderOnLangChange: true,
        prodMode: !isDevMode(),
      },
      loader: TranslocoHttpLoader,
    }),
    { provide: TRANSLOCO_LOADER, useClass: TranslocoHttpLoader },
  ],
})
export class TranslocoRootModule {}
