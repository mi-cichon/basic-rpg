using BasicRPG.Domain.Enums;

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
}