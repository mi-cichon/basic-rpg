import { Pipe, type PipeTransform } from "@angular/core";
import { TranslocoService } from "@ngneat/transloco";
import { firstValueFrom } from "rxjs";

@Pipe({
  name: "translateIfKeyExists",
  standalone: true,
})
export class TranslateIfKeyExistsPipe implements PipeTransform {
  constructor(private transloco: TranslocoService) {}

  async transform(key: string): Promise<string> {
    return await this.getTranslation(key);
  }

  private async getTranslation(key: string): Promise<string> {
    const translation = await firstValueFrom(
      this.transloco.selectTranslate(key),
    );
    return translation;
  }
}
