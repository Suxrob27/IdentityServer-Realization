using IdentityServer.Model;
using IdentityServer.Notification;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityServer.Pages.TwoFactorAuthentication
{
    public class VerifyAuthenticationCode : PageModel
    {
        [BindProperty]
        public VerifyAuthenticatorViewModel authModel { get; set; }  = new VerifyAuthenticatorViewModel();
        private readonly SignInManager<IdentityUser> signInManager;

        public VerifyAuthenticationCode(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        public async Task<IActionResult> OnGet(bool rememberMe, string returnurl = null)
        {
            var user = await signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null) {
                ViewData["Notification"] = new NotificationModel()
                {
                    Property = "Man You are ,hmmmm you are , how to say it, hmmm, I Regard that you are not registtrated Yet dude",
                    notificationType = NotificationType.Success,    
                };
            }
            authModel.ReturnURl = returnurl;    
            return Page();
        }
        public async Task<IActionResult> OnPost(string returnurl = null)
        {
           

            var result = await signInManager.TwoFactorAuthenticatorSignInAsync(authModel.Code, authModel.RememberMe, rememberClient: false);
            if (result.Succeeded)
            {
                return Redirect("/index");
            }
            if (result.IsLockedOut)
            {
                return RedirectToPage("/user/login");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }
        }
       
    }
}
