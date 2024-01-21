export type ChatMessage = {
  id: number;
  name: string;
  message: string;
  type: number;
  permission: number;
  from: boolean;
  time: string;
  value?: string;
  organisation?: string;
  punishmentLength?: number;
  additionalName?: string;
};

export function mapObjectToMessage(data: object): ChatMessage {
  return {
    id: data["id" as keyof object],
    name: data["name" as keyof object],
    message: data["message" as keyof object],
    type: data["type" as keyof object],
    permission: data["permission" as keyof object],
    from: data["from" as keyof object],
    time: data["time" as keyof object],
    value: data["value" as keyof object],
    organisation: data["organisation" as keyof object],
    punishmentLength: data["punishmentLength" as keyof object],
    additionalName: data["additionalName" as keyof object],
  } as ChatMessage;
}
