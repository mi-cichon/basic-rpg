import { Component } from "@angular/core";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.scss"],
})
export class AppComponent {
  public twojstary = false;

  public onClick(): void {
    this.twojstary = !this.twojstary;
  }
}
