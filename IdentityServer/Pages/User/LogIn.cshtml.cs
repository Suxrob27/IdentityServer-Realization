using IdentityServer.Model;
using IdentityServer.Model.ViewModel;
using IdentityServer.Notification;
using IdentityServer.Pages.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace IdentityServer.Pages.User
{
    public class LogInModel : PageModel
    {
        
        private readonly SignInManager<ApplicationUser> signInManager;

        [BindProperty]
        public SignInModel registerModel { get; set; }
        public Microsoft.AspNetCore.Identity.SignInResult SignInResult = new Microsoft.AspNetCore.Identity.SignInResult();

        public LogInModel(SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        public void OnGet(string returnurl = null)
        {
            var notification = (string)TempData["Notification"];
            if (notification != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<NotificationModel>(notification);
            }
        }
        public async Task<IActionResult> OnPostAsync(string returnurl = null)
        {

           var result =  await signInManager.PasswordSignInAsync(registerModel.Email, registerModel.Password, registerModel.RememberMe, lockoutOnFailure:true);
            if (result.Succeeded)
            {
                var notification = new NotificationModel
                {
                    Property = "Congratullations your Has Been Successfully Loged In",
                    notificationType = NotificationType.Success,    
                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);
                if (!string.IsNullOrEmpty(returnurl) && returnurl != "/User/Registration")
                {
                    ViewData["Notification"] = new NotificationModel
                    {
                        Property = "Bor Smth wRong With this,Think About This",
                        notificationType = NotificationType.Failure
                    };
                    return Redirect(returnurl);
                }
                else
                {
                    // Handle invalsid returnUrl (e.g., log a warning or redirect to a default page)
                    return LocalRedirect("/Index"); // Redirect to home page
                }
            }
            if (result.RequiresTwoFactor)
            {
                return RedirectToPage("/TwoFactorAuthentication/VerifyAuthenticationCode",returnurl);
            }
            if (result.IsLockedOut)
            {
               
                SignInResult = result;
                return Page();
            }
            else
            {
                ModelState.AddModelError(string.Empty,"Invlaid Login Attempt");
                return Page();
            }
        }
    }
}
