import { Component, OnDestroy, OnInit } from "@angular/core";
import {
  PlayerInfoValues,
  mapObjectToPlayerInfoValue,
} from "./player-info.model";
import { AbstractClientApiService } from "src/app/lib/client-api-service/abstract-client-api.service";
import { Subscription } from "rxjs";

@Component({
  selector: "app-player-info",
  templateUrl: "./player-info.component.html",
  styleUrls: ["./player-info.component.scss"],
})
export class PlayerInfoComponent implements OnInit, OnDestroy {
  public hudValues$!: Subscription;
  public hudValues: PlayerInfoValues | null = null;

  constructor(private clientApiService: AbstractClientApiService) {}

  ngOnDestroy(): void {
    this.hudValues$.unsubscribe();
  }

  ngOnInit(): void {
    this.hudValues$ = this.clientApiService
      .registerEvent("browser_updatePlayerInfo")
      .subscribe((response) => {
        if (response.data) {
          this.hudValues = mapObjectToPlayerInfoValue(response.data);
        }
      });
  }
}
