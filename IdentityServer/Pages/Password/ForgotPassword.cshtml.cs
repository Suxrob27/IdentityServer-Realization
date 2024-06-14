using IdentityServer.Context;
using IdentityServer.Model;
using IdentityServer.Model.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace IdentityServer.Pages.User
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManger;
        private readonly ApplicationDB db;
        private readonly IConfiguration configuration;
        private readonly IOptions<Smptsetting> smptSetting;
        private readonly ResetPassword resetPassword;
        public ForgotPasswordModel(UserManager<IdentityUser> userManger, ApplicationDB _db, IConfiguration configuration, IOptions<Smptsetting> smptSetting, ResetPassword resetPassword)
        {
         
            this.userManger = userManger;
            db = _db;
            this.configuration = configuration;
            this.smptSetting = smptSetting;
            this.resetPassword = resetPassword;
        }
        [BindProperty]
        public ResetPassword forgotPassword { get; set; } 
        public void OnGet()
        {

        }
        [HttpPost]
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await db.Users.Where(x => x.Email == forgotPassword.Email).FirstOrDefaultAsync();
            if( user != null)
            {
                resetPassword.Email = forgotPassword.Email;
                var code = await userManger.GeneratePasswordResetTokenAsync(user);
            
                var confirmationLink = Url.PageLink(pageName: "/Password/ResetPasswrod",
                    values: new { userId = user.Id, code});

                var message = new MailMessage("suxrobvjl1@gmail.com",
                    user.Email,
                    "Reset The Password",
                    $"Please Click on this Link To Reset Your Password : {confirmationLink}");

                using (var emailClient = new SmtpClient(smptSetting.Value.Server, smptSetting.Value.Port))
                {
                    emailClient.Credentials = new NetworkCredential(
                        smptSetting.Value.Login,
                        smptSetting.Value.Password);

                    await emailClient.SendMailAsync(message);
                }
                ViewData["SuccessSend"] = "The resset page Was Sended to Your Email. Now you can reset Your Password";
                return RedirectToPage("/user/login");

            }
            if (user == null)
            {
                ViewData["ErrorUser"] = "The User Was not Found";
            }
            else
            {
                ViewData["ErrorUser"] = "Happened Some Error Try it Again Or Later";
            }
            return RedirectToPage("/user/login");

        }

    }
}
