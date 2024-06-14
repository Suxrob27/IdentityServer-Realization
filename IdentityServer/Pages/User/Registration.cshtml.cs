using IdentityServer.Model.ViewModel;
using IdentityServer.Notification;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Text.Json;

namespace IdentityServer.Pages.User
{
    public class RegistrationModel : PageModel
    {
        [BindProperty]
         public RegisterModel regModel { get; set; }   


        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public RegistrationModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public void OnGet(string returnUrl = null)
        {

        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = regModel.Name,
                    Email = regModel.Email,
                };

                var result = await userManager.CreateAsync(user, regModel.Password);   
                if(result.Succeeded) 
                {
                    var notification = new NotificationModel
                    {
                        Property = "Congratullations your Has Been Successfully Loged In",
                        notificationType = NotificationType.Success,
                    };
                    TempData["Notification"] = JsonSerializer.Serialize(notification);

                    await signInManager.SignInAsync(user, isPersistent: false);
                    return Redirect(returnUrl);
                }
                AddErrors(result);
            }
            return Page();

        }

        private void AddErrors(IdentityResult result)
        {

            foreach (var errors in result.Errors)
            {
                ModelState.AddModelError(string.Empty, errors.Description);
            }
        }
     }
}
