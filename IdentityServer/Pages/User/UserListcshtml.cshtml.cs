using IdentityServer.Context;
using IdentityServer.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityServer.Pages.User
{
    public class UserListcshtmlModel : PageModel
    {
        private readonly ApplicationDB db;
        private readonly UserManager<IdentityUser> userManager;
      public List<ApplicationUser> userList = new List<ApplicationUser>();

        public UserListcshtmlModel(ApplicationDB db, UserManager<IdentityUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }
        public async Task<IActionResult>  OnGet()
        {
             userList = db.ApplicationUser.ToList();   

            foreach (var user in userList) 
            {
              var userRole = await userManager.GetRolesAsync(user) as List<string>;
              user.Role = String.Join(",", userRole);   
           
             var user_claim = userManager.GetClaimsAsync(user);
             user.UserClaim = string.Join(",", user_claim);
            }
            return Page();
        }
    }
}
