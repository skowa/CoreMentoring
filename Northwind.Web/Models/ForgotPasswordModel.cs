using System.ComponentModel.DataAnnotations;

namespace Northwind.Web.Models
{
    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
