export type SpeedometerData = {
  speed: number;
  rpm: number;
  petrol: number;
  trip: number;
};

export function mapObjectToSpeedometerData(data: object) {
  return {
    speed: data["speed" as keyof object],
    rpm: data["rpm" as keyof object],
    petrol: data["petrol" as keyof object],
    trip: data["trip" as keyof object],
  } as SpeedometerData;
}
