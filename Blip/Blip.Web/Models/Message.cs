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

        public virtual ICollection<User> Senders { get; set; }
        public virtual ICollection<User> Receivers { get; set; }

        public Message ()
        {
            Senders = new HashSet<User>();
            Receivers = new HashSet<User>();
        }
    }
}