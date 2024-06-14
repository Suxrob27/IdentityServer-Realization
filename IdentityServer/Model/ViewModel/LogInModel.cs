using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Model.ViewModel
{
    public class SignInModel
    {
        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; } 
    }
}
