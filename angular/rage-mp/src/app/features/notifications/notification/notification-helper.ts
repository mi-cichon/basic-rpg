import { ApiResponseType } from "src/app/lib/client-api-service/client-api.model";

export function mapApiResponseTypeToNotificationType(
  responseType: ApiResponseType,
): NotificationType {
  switch (responseType) {
    case "success":
      return 0;
    case "fail":
      return 3;
    case "exception":
      return 4;
  }
  return 4;
}

export type NotificationType = 0 | 1 | 2 | 3 | 4;
