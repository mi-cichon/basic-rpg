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

const petrolSteps = [
  0, 3.5, 7, 10.5, 14, 17.5, 21, 24.5, 28, 31.5, 35, 38.5, 42, 45.5, 49, 52.5,
  56, 59.5, 63, 66.5, 70, 73.5, 77, 80.5, 84, 87.5, 92, 95.5, 100,
];

export function getPetrolStep(petrol: number): string {
  petrol = petrol * 100;
  for (const step of petrolSteps) {
    if (petrol <= step) {
      return step.toString().replaceAll(".", ",");
    }
  }
  return "0";
}
