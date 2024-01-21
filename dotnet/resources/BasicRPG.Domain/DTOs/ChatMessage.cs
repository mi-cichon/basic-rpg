using BasicRPG.Domain.Enums;
// ReSharper disable InconsistentNaming

namespace BasicRPG.Domain.DTOs;

public class ChatMessage
{
    public int? id { get; set; }
    public string? name { get; set; }
    public string? message { get; set; }
    public MessageType? type { get; set; }
    public Permissions? permission { get; set; }
    public bool? from { get; set; }
    public string? time { get; set; }
    public string? value { get; set; }
    public string? additionalName { get; set; }
    public string? punishmentLength { get; set; }
    public string? organisation { get; set; }
}