using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityServer.Pages.TwoFactorAuthentication
{
    public class ResetTwoFactorAuthModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;

        public ResetTwoFactorAuthModel(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<IActionResult> OnGet()
        {
            var user = await userManager.GetUserAsync(User);
            await userManager.ResetAuthenticatorKeyAsync(user);
            await userManager.SetTwoFactorEnabledAsync(user, false);
            return RedirectToPage("/Index");
        }
    }
}
