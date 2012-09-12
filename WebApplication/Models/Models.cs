using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace WebApplication.Models
{
    public class DailyVisitors
    {
        public string Day { get; set; }
        public int Count { get; set; }
    }

    public class MonthlyVisitors
    {
        public string Month { get; set; }
        public int Count { get; set; }
    }

    public class RssModel
    {
        public string Title { get; set; }
        public IEnumerable<XElement> Elements { get; set; }
    }

    public class Rss
    {
        public string Link { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class AdminModel
    {
        public int AllEntriesCount { get; set; }
        public int AllEntriesCountInThisMonth { get; set; }
        public int NewMessagesCount { get; set; }
    }

    public class ForgottenPasswordModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Common), ErrorMessageResourceName = "Required")]
        [EmailAddressAttribute]
        public string ForEmailAddress { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Common), ErrorMessageResourceName = "Required")]
        public string ForUserName { get; set; }
    }

    public class FeedbackModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Common), ErrorMessageResourceName = "Required")]
        [EmailAddressAttribute]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

        public string CaptchaText { get; set; }
    }

    public class LoginModel
    {
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }

    public class InstallModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Common), ErrorMessageResourceName = "Required")]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Common), ErrorMessageResourceName = "Required")]
        [EmailAddressAttribute]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Common), ErrorMessageResourceName = "Required")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.Common), ErrorMessageResourceName = "PasswordValidation", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessageResourceType = typeof(Resources.Common), ErrorMessageResourceName = "ConfirmPasswordValidation")]
        public string ConfirmPassword { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "Username")]
        public string RegUserName { get; set; }

        [Required]
        [EmailAddressAttribute]
        [Display(Name = "Email address")]
        public string RegEmail { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.Common), ErrorMessageResourceName = "PasswordValidation", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string RegPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("RegPassword", ErrorMessageResourceType = typeof(Resources.Common), ErrorMessageResourceName = "ConfirmPasswordValidation")]
        public string RegConfirmPassword { get; set; }

        public string RegCaptchaText { get; set; }
    }

    public class NewEmailModel
    {

        [Required]
        [EmailAddressAttribute]
        [Display(Name = "Email address")]
        public string NewEmail { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.Common), ErrorMessageResourceName = "PasswordValidation", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string EmailOldPassword { get; set; }
    }

    public class NewPasswordModel
    {
        [Required]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.Common), ErrorMessageResourceName = "PasswordValidation", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.Common), ErrorMessageResourceName = "PasswordValidation", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("NewPassword", ErrorMessageResourceType = typeof(Resources.Common), ErrorMessageResourceName = "ConfirmPasswordValidation")]
        public string NewConfirmPassword { get; set; }
    }
}