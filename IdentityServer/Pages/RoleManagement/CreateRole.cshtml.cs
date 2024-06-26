using IdentityServer.Context;
using IdentityServer.Model.Role;
using IdentityServer.Notification;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using System.Runtime.CompilerServices;

namespace IdentityServer.Pages.RoleManagement
{
    public class CreateRoleModel : PageModel
    {
        private readonly ApplicationDB _db;
        private readonly RoleManager<IdentityRole> roleManager;
        [BindProperty]
        public IdentityRole IdentityRole { get; set; }          
        public CreateRoleModel(ApplicationDB db, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            this.roleManager = roleManager;
        }

        public IActionResult OnGet(string roleId)
        { 
                if (String.IsNullOrEmpty(roleId))
                {
                    //create
                    return Page();
                }
                else
                {
                    //update
                    IdentityRole = _db.Roles.FirstOrDefault(u => u.Id == roleId);
                    return Page();
                }
        }   
        


        public async  Task<IActionResult> OnPost(string roleId)
        {
            if (string.IsNullOrEmpty(roleId))
            {
                await roleManager.CreateAsync(new IdentityRole() { Name = IdentityRole.Name });
                ViewData["Notification"] = new NotificationModel 
                {
                 Property = "Broooooo The new Role has been added",
                 notificationType = NotificationType.Success 
                };
                return RedirectToPage("/RoleManagement/RoleList");

            }
            else
            {
                //update
                var objFromDb = _db.Roles.FirstOrDefault(u => u.Id == roleId) ?? new IdentityRole() ;
                objFromDb.Name = IdentityRole.Name; 
                objFromDb.NormalizedName = IdentityRole.Name.ToUpper(); 
                var result = await roleManager.UpdateAsync(objFromDb);
                ViewData["Notification"] = new NotificationModel
                {
                    Property = "Broooooo The  Role has been updated",
                    notificationType = NotificationType.Success
                };

                return RedirectToPage("/RoleManagement/RoleList");
            }
        }
    }
}
