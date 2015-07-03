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
        public ICollection<UserIC> ListOfUsers { get; set; }

        public class UserIC
        {
            //[Display(Name = "User ID")]
            public int UserID { get; set; }

            //[Display(Name = "User Name")]
            public string UserName { get; set; }

            public string Password { get; set; }

            public string Role { get; set; }

            public bool Active { get; set; }

            //[Display(Name = "Active Date")]
            public DateTime ActiveDate { get; set; }
        }

        //The following properties are added just for Data Annotation use.////////////////////////////
        //@Html.DisplayNameFor cannot display the data annotation if the inner class value is null////
        // TODO Find out if there is a way to use inner class data annotation even if it is null
        [Display(Name = "User ID")]
        public int UserID { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public bool Active { get; set; }

        [Display(Name = "Active Date")]
        public DateTime ActiveDate { get; set; }
        //////////////////////////////////////////////////////////////////////////////////////////////
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
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public string Role { get; set; }
    }

    public class AccountEditViewModel
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
}