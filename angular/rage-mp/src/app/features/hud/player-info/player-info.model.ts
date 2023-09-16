export type PlayerInfoValues = {
  money: string;
  name: string;
  experience: number;
  nextLevelExperience: number | null;
  level: number;
};

export function mapObjectToPlayerInfoValue(data: object) {
  return {
    money: (data["money" as keyof object] as number).toFixed(2),
    name: data["name" as keyof object],
    experience: data["experience" as keyof object],
    nextLevelExperience: data["nextLevelExperience" as keyof object],
    level: data["level" as keyof object],
  } as PlayerInfoValues;
}
