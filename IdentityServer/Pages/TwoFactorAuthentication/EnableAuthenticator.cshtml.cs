using IdentityServer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace IdentityServer.Pages.TwoFactorAuthentication
{
    [Authorize]
    [ValidateAntiForgeryToken]
    public class EnableAuthenticatorModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly TwoFactorAuthenticationViewModel twoFactorAuthentication;
        [BindProperty]
        public TwoFactorAuthenticationViewModel tfAuthenticationModel { get; set; } 
        public EnableAuthenticatorModel(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<IActionResult> OnGet()
        {
            var user = await userManager.GetUserAsync(User);    
            await userManager.ResetAuthenticatorKeyAsync(user); 
            var token = await userManager.GetAuthenticatorKeyAsync(user);
            tfAuthenticationModel = new TwoFactorAuthenticationViewModel() { Token = token };

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
