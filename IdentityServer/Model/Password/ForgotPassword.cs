using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Model.Model
{
    public class ForgotPassword
    {
        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Code {  get; set; }  
    }
}
