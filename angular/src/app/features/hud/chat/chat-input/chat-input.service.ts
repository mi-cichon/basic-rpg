import { Injectable } from "@angular/core";
@Injectable({
  providedIn: "root",
})
export class ChatInputService {
  public sentMessages: string[] = [];
}
