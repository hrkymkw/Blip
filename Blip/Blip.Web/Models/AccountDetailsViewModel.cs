using System;

namespace Blip.Web.Models
{
    public class AccountDetailsViewModel
    {
        public int UserID { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public bool Active { get; set; }

        public DateTime ActiveDate { get; set; }
    }
}