using System.ComponentModel.DataAnnotations;

namespace Northwind.Web.Models
{
    public class ExternalLoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string ProviderDisplayName { get; set; }

        public string ReturnUrl { get; set; }
    }
}
