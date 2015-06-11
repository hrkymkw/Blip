using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blip.Web.Models
{
    public class Message
    {
        public int MessageID { get; set; }
        public string Title { get; set; }
        public DateTime DateTime { get; set; }
        public string Body { get; set; }
        public int SenderID { get; set; }

        public virtual User Sender { get; set; }
        public virtual ICollection<User> Receivers { get; set; }

        public Message ()
        {
            Receivers = new HashSet<User>();
        }
    }
}