export type ApiResponse = {
  responseType: ApiResponseType;
  message: string;
  data?: object;
};

export type ApiResponseType = "success" | "fail" | "exception";

export function mapApiResponseTypeToResponseType(
  apiResponseType: number,
): ApiResponseType {
  switch (apiResponseType) {
    case 0:
      return "success";
    case 1:
      return "fail";
    case 2:
      return "exception";
  }
  return "exception";
}
