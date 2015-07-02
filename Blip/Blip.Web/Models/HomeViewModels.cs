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

    public class HomeMessageViewModel
    {
        public ICollection<ReceiverIC> Receivers { get; set; }

        public MessageIC Message { get; set; }

        public class ReceiverIC
        {
            [Display(Name = "User ID")]
            public int UserID { get; set; }

            [Display(Name = "User Name")]
            public string UserName { get; set; }
        }

        public class MessageIC
        {
            [Required(ErrorMessage = "You must provide Title.")]
            public string Title { get; set; }

            [Required(ErrorMessage = "Message cannot be empty.")]
            [DataType(DataType.MultilineText)]
            public string Body { get; set; }

            public string Sender { get; set; }

            [Required(ErrorMessage = "You must select recipients.")]
            public List<int> Receivers { get; set; }
        }
    }

}