namespace Telerik.Web.Mvc.Examples.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class FeedbackMessage
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [AllowHtml]
        public string Comment { get; set; }
    }
}
