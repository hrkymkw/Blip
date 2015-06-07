using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blip.Web.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool Active { get; set; }
        public DateTime ActiveDate { get; set; }

        public virtual ICollection<MessagePacket> SentMessagePackets { get; set; }
        public virtual ICollection<MessagePacket> ReceivedMessagePackets { get; set; }
    }
}