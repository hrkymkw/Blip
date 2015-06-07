using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blip.Web.Models
{
    public class MessagePacket
    {
        public int MessagePacketID { get; set; }
        public DateTime DateTime { get; set; }
        public int MessageID { get; set; }
        public int SenderID { get; set; }
        public int ReceiverID { get; set; }

        public virtual Message Message { get; set; }
        public virtual User Sender { get; set; }
        public virtual User Receiver { get; set; }
    }
}