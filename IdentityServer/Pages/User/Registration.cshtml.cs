using IdentityServer.Model.ViewModel;
using IdentityServer.Notification;
using IdentityServer.Servises;
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
        private readonly IEmailSender emailSender;

        public RegistrationModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
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
                    UserName = regModel.Email,
                    Email = regModel.Email,
                    NormalizedUserName = regModel.Name
                };

                var result = await userManager.CreateAsync(user, regModel.Password);
                if (result.Succeeded)
                {
                    var notification = new NotificationModel
                    {
                        Property = "Broo Now Check Your Email And Confirm it, Leetsss Gooo",
                        notificationType = NotificationType.Success,
                    };
                    TempData["Notification"] = JsonSerializer.Serialize(notification);

                    var confirmToken = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.PageLink(pageName: "/ResetEmail/ConfirmationEmail",
                        values: new { userId = user.Id, token = confirmToken }) ?? "";

                    await emailSender.SendAsync("suxrobvjl1@gmail.com",
                        user.Email,
                        "Please Confirm Your Email",
                        $"Please click on this link to confirm your email address : {confirmationLink}");
                        return RedirectToPage("/User/Login");
                        }
                else
                {

                    AddErrors(result);
                }
                   
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
