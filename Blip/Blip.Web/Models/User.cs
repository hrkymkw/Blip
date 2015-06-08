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

        public virtual ICollection<Message> ReceivedMessages { get; set; }

        public User ()
        {
            ReceivedMessages = new HashSet<Message>();
        }
    }
}