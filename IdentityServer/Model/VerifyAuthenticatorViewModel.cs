using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Model
{
    public class VerifyAuthenticatorViewModel
    {
        [Required]
        public string Code { get; set; }    
        public string ReturnURl {  get; set; }
        [Display(Name ="Remember Me?")]
        public bool RememberMe { get; set; }        
    }
}
