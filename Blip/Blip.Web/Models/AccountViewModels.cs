using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blip.Web.Models
{
    public class AccountLoginViewModel
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }

    public class AccountRegisterViewModel
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class AccountIndexViewModel
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

        public ICollection<string> SentMessages { get; set; }

        public ICollection<string> ReceivedMessages { get; set; }
    }

    public class AccountDetailsViewModel
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

    public class AccountCreateViewModel
    {
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }

    public class AccountEditViewModel
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