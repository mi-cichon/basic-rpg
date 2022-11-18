using Serverside.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serverside.DTO
{
    public class MessageDTO
    {
        public int? id { get; set; }
        public string? name { get; set; }
        public string? message { get; set; }
        public MessageType? type { get; set; }
        public Permissions? permission { get; set; }
        public bool? from { get; set; }
        public string? time { get; set; }
    }
}
