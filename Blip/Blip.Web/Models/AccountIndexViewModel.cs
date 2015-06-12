using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blip.Web.Models
{
    public class AccountIndexViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public bool Active { get; set; }
        public DateTime ActiveDate { get; set; }

        public string[] SentMessages { get; set; }
        public string[] ReceivedMessages { get; set; }
    }
}