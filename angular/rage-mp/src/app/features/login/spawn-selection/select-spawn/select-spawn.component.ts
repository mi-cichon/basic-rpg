import { Component, EventEmitter, Input, Output } from "@angular/core";

@Component({
  selector: "app-select-spawn",
  templateUrl: "./select-spawn.component.html",
  styleUrls: ["./select-spawn.component.scss"],
})
export class SelectSpawnComponent {
  @Input({ required: true }) spawnId!: number;
  @Input({ required: true }) spawnName!: string;

  @Output() spawnSelectedEvent = new EventEmitter<number>();

  spawnSelected() {
    this.spawnSelectedEvent.emit(this.spawnId);
  }
}
