import { Component } from "@angular/core";
import { NotificationService } from "./features/notifications/notification/notification.service";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.scss"],
})
export class AppComponent {
  constructor(public notificationService: NotificationService) {}
}
