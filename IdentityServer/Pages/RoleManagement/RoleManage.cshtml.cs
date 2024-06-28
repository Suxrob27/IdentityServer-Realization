using IdentityServer.Model;
using IdentityServer.Model.Role;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityServer.Pages.RoleManagement
{
    public class RoleManageModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleManageModel(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IActionResult> OnGet(string userId)
        {
            ApplicationUser user = (ApplicationUser)await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            List<string> exsitingUserRole = await userManager.GetRolesAsync(user) as List<string>;
            var model = new RoleViewsModel()
            { 
              User = user
            };
            foreach (var roles in roleManager.Roles)
            {
                RoleModel roleSelection = new()
                {
                    RoleName = roles.Name,
                };
                if (exsitingUserRole.Any(x => x == roles.Name))
                {
                    roleSelection.IsRoleSelected = true;
                }
                model.Roles.Add(roleSelection); 
            }
            return RedirectToPage("/Index");


        }
    }
}
