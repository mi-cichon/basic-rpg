import { Observable } from "rxjs";
import { ApiResponse } from "./client-api.model";

export abstract class AbstractClientApiService {
  abstract callClient(eventName: string, data?: unknown): Promise<ApiResponse>;
  abstract registerEvent(eventName: string): Observable<ApiResponse>;
  abstract pokeClient(eventName: string, data?: unknown): void;
}
