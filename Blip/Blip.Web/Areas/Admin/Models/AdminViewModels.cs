using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blip.Web.Areas.Admin.Models
{
    public class AdminIndexViewModel
    {
        public ICollection<UserIC> ListOfUsers { get; set; }

        public class UserIC
        {
            [Display(Name = "User ID")]
            public int UserID { get; set; }

            [Display(Name = "User Name")]
            public string UserName { get; set; }

            public string Password { get; set; }

            public string Role { get; set; }

            public bool Active { get; set; }

            [Display(Name = "Active Date")]
            public DateTime ActiveDate { get; set; }
        }
    }

    public class AdminDetailsViewModel
    {
        [Display(Name = "User ID")]
        public int UserID { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public bool Active { get; set; }

        [Display(Name = "Active Date")]
        public DateTime ActiveDate { get; set; }
    }

    public class AdminCreateViewModel
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public string Role { get; set; }
    }

    public class AdminEditViewModel
    {
        [Display(Name = "User ID")]
        public int UserID { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public string Role { get; set; }

        public bool Active { get; set; }

        [Required]
        [Display(Name = "Active Date")]
        public DateTime ActiveDate { get; set; }
    }

    public class AdminDeleteViewModel
    {
        [Display(Name = "User ID")]
        public int UserID { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public bool Active { get; set; }

        [Display(Name = "Active Date")]
        public DateTime ActiveDate { get; set; }
    }
}