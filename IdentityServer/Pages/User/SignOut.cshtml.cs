using IdentityServer.Notification;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace IdentityServer.Pages.User
{
    public class SignOutModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;

        public SignOutModel(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        public async Task<IActionResult> OnGet()
        {
            await signInManager.SignOutAsync();
            var notification = new NotificationModel
            {
                Property = "You've signed Out Man Continue To Work HARD",
                notificationType = NotificationType.Success,
            };
            TempData["Notification"] = JsonSerializer.Serialize(notification);
            return RedirectToPage("/Index");
        }
        
    }
}
