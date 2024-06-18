using IdentityServer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text;
using System.Text.Encodings.Web;

namespace IdentityServer.Pages.TwoFactorAuthentication
{
    [Authorize]
    [ValidateAntiForgeryToken]
    public class EnableAuthenticatorModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly UrlEncoder _urlencoder;
        private readonly TwoFactorAuthenticationViewModel twoFactorAuthentication;
        [BindProperty]
        public TwoFactorAuthenticationViewModel tfAuthenticationModel { get; set; } 
        public EnableAuthenticatorModel(UserManager<IdentityUser> userManager, UrlEncoder urlencoder)
        {
            this.userManager = userManager;
            _urlencoder = urlencoder;
        }
        public async Task<IActionResult> OnGet()
        {
            string AuthUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";
            var user = await userManager.GetUserAsync(User);    
            await userManager.ResetAuthenticatorKeyAsync(user); 
            var token = await userManager.GetAuthenticatorKeyAsync(user);
            string AuthUri = string.Format(AuthUriFormat,_urlencoder.Encode("IdentityManager"), _urlencoder.Encode(user.Email),token);
            tfAuthenticationModel = new TwoFactorAuthenticationViewModel() { Token = token, QRCodeUrl = AuthUri };

            return Page();
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await userManager.GetUserAsync(User);
            
                var successed = await userManager.VerifyTwoFactorTokenAsync(user, userManager.Options.Tokens.AuthenticatorTokenProvider, tfAuthenticationModel.Code);
                if (successed)
                {
                    await userManager.SetTwoFactorEnabledAsync(user,true);
                    return RedirectToPage("/Privacy");

                }
                ModelState.AddModelError("Verify", "Your two factor auth code could not be validated");
                return RedirectToPage("/Index");
            
         
        }
    }
}
