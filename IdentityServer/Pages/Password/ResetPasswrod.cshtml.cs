using IdentityServer.Context;
using IdentityServer.Model.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Pages.Password
{
    public class ResetPasswrodModel : PageModel
    {
        
        private readonly UserManager<IdentityUser> userManager;
        private readonly ApplicationDB db;

        [BindProperty]
        public  ResetPassword resetPassword { get; set; }
        private static string Email { get; set; }
        public ResetPasswrodModel(UserManager<IdentityUser> userManager, ApplicationDB _db, ResetPassword resetPassword)
        {
            this.userManager = userManager;
            db = _db;
            this.resetPassword = resetPassword;
        }
        public void OnGet()
        {
            Email = resetPassword.Email;
        }
        public async Task<IActionResult> OnPostAsync(string code = null)
        {

           var user =  await db.Users.Where(x => x.Email == Email).FirstOrDefaultAsync();

            var result = await userManager.ResetPasswordAsync(user, code, resetPassword.Password);
            
            return RedirectToAction("/Index");   
        }
    }
}
