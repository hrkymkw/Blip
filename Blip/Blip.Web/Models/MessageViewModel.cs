using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blip.Web.Models
{
    public class MessageViewModel
    {
        public int MessageID { get; set; }
        public string Title { get; set; }
        public DateTime DateTime { get; set; }
        public string Body { get; set; }
        public User Sender { get; set; }
        public ICollection<User> Receivers { get; set; }
    }
}