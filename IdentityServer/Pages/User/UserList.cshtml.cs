using IdentityServer.Context;
using IdentityServer.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityServer.Pages.User
{
    public class UserListModel : PageModel
    {
        private readonly ApplicationDB db;
        public List<ApplicationUser> userList;

        public UserListModel(ApplicationDB db, UserManager<ApplicationUser> userManager)
        {
            this.db = db;
        }
        public IActionResult OnGet()
        {
            userList = db.ApplicationUser.ToList();
            var userRole = db.UserRoles.ToList();
            var roles = db.Roles.ToList();

            foreach (var user in userList)
            {
                var user_role =  userRole.FirstOrDefault(u => u.UserId == user.Id);
                if (user_role == null) 
                {
                    user.Role = "none";
                }
                else
                {
                    user.Role =  roles.FirstOrDefault(u => u.Id == user_role.RoleId).Name;
                }

            }
            return Page();

        }
    }
}
