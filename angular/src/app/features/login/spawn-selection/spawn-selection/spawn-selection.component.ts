import { Component } from "@angular/core";
import { LoginService } from "../../login.service";
import { NotificationService } from "src/app/features/notifications/notification/notification.service";
import { Router } from "@angular/router";

@Component({
  selector: "app-spawn-selection",
  templateUrl: "./spawn-selection.component.html",
  styleUrls: ["./spawn-selection.component.scss"],
})
export class SpawnSelectionComponent {
  constructor(
    public loginService: LoginService,
    private notificationService: NotificationService,
    private router: Router,
  ) {}

  public spawnSelected(spawnId: number) {
    this.loginService.selectSpawn(spawnId).then((response) => {
      if (response.responseType === "success") {
        this.goToHudComponent();
      } else {
        this.notificationService.showNotification(
          response.message,
          response.responseType,
        );
      }
    });
  }

  public goToHudComponent(): void {
    this.router.navigateByUrl("/hud");
  }
}
