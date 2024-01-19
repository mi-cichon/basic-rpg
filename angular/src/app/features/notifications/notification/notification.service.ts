import { Injectable } from "@angular/core";
import { ApiResponseType } from "src/app/lib/client-api-service/client-api.model";
import { NotificationType } from "./notification-helper";
import { mapApiResponseTypeToNotificationType } from "./notification-helper";

@Injectable({
  providedIn: "root",
})
export class NotificationService {
  public hideNotification = true;
  public disappear = true;
  public notification?: {
    type: NotificationType;
    message: string;
  };

  public showNotification(
    message: string,
    type: ApiResponseType | NotificationType,
  ): void {
    if (isNaN(parseInt(type.toString()))) {
      this.displayNotification(
        message,
        mapApiResponseTypeToNotificationType(type as ApiResponseType),
      );
      return;
    }
    this.displayNotification(message, type as NotificationType);
  }

  private displayNotification(message: string, type: NotificationType): void {
    if (!this.disappear) {
      return;
    }
    this.hideNotification = false;
    this.disappear = false;
    this.notification = {
      message: message,
      type: type,
    };
    setTimeout(() => {
      this.hideNotification = true;
      setTimeout(() => {
        this.disappear = true;
        this.notification = undefined;
      }, 500);
    }, 4000);
  }
}
