using IdentityServer.Model;
using IdentityServer.Model.Role;
using IdentityServer.Model.ViewModel;
using IdentityServer.Notification;
using IdentityServer.Servises;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using System.Text.Json;


namespace IdentityServer.Pages.User
{
    [AllowAnonymous]

    public class RegistrationModel : PageModel
    {
        [BindProperty]
         public RegisterModel regModel { get; set; }   


        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IEmailSender emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RegistrationModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,IEmailSender emailSender, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> OnGet(string returnUrl = null)
        {
            if (!_roleManager.RoleExistsAsync(SD.Admin).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new IdentityRole(SD.Admin));
                await _roleManager.CreateAsync(new IdentityRole(SD.User));
            }

            regModel = new RegisterModel()
            {
                RoleList = _roleManager.Roles.Select(x => x.Name).Select(i => new SelectListItem
                {
                    Text = i,
                    Value = i   
                })
            };


            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = regModel.Email,
                    Email = regModel.Email,
                    NormalizedUserName = regModel.Name
                };

                var result = await userManager.CreateAsync(user, regModel.Password);
                if (result.Succeeded)
                { 
                        if (regModel.RoleSelected != null && regModel.RoleSelected.Length > 0 && regModel.RoleSelected == SD.Admin)
                        {
                           await userManager.AddToRoleAsync(user, SD.Admin);
                        }
                        else
                        {
                           await userManager.AddToRoleAsync(user, SD.User);
                        }

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

                    regModel.RoleList = _roleManager.Roles.Select(x => x.Name).Select(i => new SelectListItem
                    {
                     Text = i,
                     Value = i
                    });
                   


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
