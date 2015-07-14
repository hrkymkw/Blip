using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Blip.Web.Models
{
    public class HomeIndexViewModel
    {
        public ICollection<MessageIC> Messages { get; set; }

        public IEnumerable<string> SearchByEnum { get; set; }

        public ICollection<SelectListItem> SearchBy { get; set; }

        public string CurrentSearchBy { get; set; }
        public string CurrentSearchString { get; set; }
        public string DateSortParm { get; set; }
        public string TitleSortParm { get; set; }
        public string SenderSortParm { get; set; }

        public class MessageIC
        {
            [Display(Name = "Message ID")]
            public int MessageID { get; set; }

            public string Title { get; set; }

            [Display(Name = "Date")]
            [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy hh:mm:ss ddd}", ApplyFormatInEditMode = true)]
            public DateTime DateTime { get; set; }

            public string Body { get; set; }

            public string Sender { get; set; }

            public ICollection<string> Receivers { get; set; }
        }
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