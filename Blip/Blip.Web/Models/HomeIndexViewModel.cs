using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blip.Web.Models
{
    public class HomeIndexViewModel
    {
        [Display(Name = "Message ID")]
        public int MessageID { get; set; }

        public string Title { get; set; }

        [Display(Name = "Date")]
        public DateTime DateTime { get; set; }

        public string Body { get; set; }

        public string Sender { get; set; }

        public ICollection<string> Receivers { get; set; }
    }
}