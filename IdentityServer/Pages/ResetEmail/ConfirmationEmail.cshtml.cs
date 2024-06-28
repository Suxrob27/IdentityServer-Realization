using IdentityServer.Model;
using IdentityServer.Notification;
using IdentityServer.Servises;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Text.Json;

namespace IdentityServer.Pages.ResetEmail
{
    public class ConfirmationEmailModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public ConfirmationEmailModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public async Task<IActionResult> OnGet(string userId, string token)
        {
            var user = await userManager.FindByIdAsync(userId);

            var result = await userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
               await signInManager.SignInAsync(user, false);

                var notification = new NotificationModel
                {
                    Property = "Cooooongrraaaaatulllatiiionnnsss💪💪💪🎉🎉🎉🎉🎉 Maaaaannn Your Account Has Been Cooonfffirmmmeeedd.KKKEEEEEEEEEPP UUUUUUUPPP",
                    notificationType = NotificationType.Success
                    
                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);   
                
                return RedirectToPage("/Index");
            }
            else
            {
                var notification = new NotificationModel
                {
                    Property = "Smth Went Wrong Bro.But get Frustrated Try it Again And Do Not Give Up Man",
                    notificationType = NotificationType.Failure
                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);

                return RedirectToPage("User/Login");
            }
        }




    }
}
