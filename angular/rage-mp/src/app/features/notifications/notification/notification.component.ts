import { Component, Input, OnInit } from "@angular/core";
import { NotificationService } from "./notification.service";
import { NotificationType } from "./notification-helper";

@Component({
  selector: "app-notification",
  templateUrl: "./notification.component.html",
  styleUrls: ["./notification.component.scss"],
})
export class NotificationComponent implements OnInit {
  constructor(public notificationService: NotificationService) {}

  public type!: NotificationType;
  public message!: string;
  public color = "#59a310";

  ngOnInit(): void {
    if (!this.notificationService.notification) {
      return;
    }
    this.type = this.notificationService.notification.type;
    this.message = this.notificationService.notification.message;
    console.log(this.type);
    switch (this.type) {
      case 0:
        this.color = "#0f9861";
        break;
      case 1:
        this.color = "#137cbd";
        break;
      case 2:
        this.color = "#fdfd4a";
        break;
      case 3:
        this.color = "#d13812";
        break;
      case 4:
        this.color = "#d9822b";
        break;
    }
  }
}
