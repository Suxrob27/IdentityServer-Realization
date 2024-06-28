using IdentityServer.Context;
using IdentityServer.Model.Role;
using IdentityServer.Notification;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using System.Runtime.CompilerServices;
using System.Text.Json;

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
                await roleManager.CreateAsync(new IdentityRole { Name = IdentityRole.Name});
                var notification = ViewData["Notification"] = new NotificationModel
                {
                    Property = "The Role Was Created SuccessFully",
                    notificationType = NotificationType.Success

                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);
                return RedirectToPage("/RoleManagement/RoleList");

            }
            else
            {
                //update
                var objFromDb = _db.Roles.FirstOrDefault(u => u.Id == roleId) ?? new IdentityRole() ;
                objFromDb.Name = IdentityRole.Name; 
                objFromDb.NormalizedName = IdentityRole.Name.ToUpper(); 
                var result = await roleManager.UpdateAsync(objFromDb);
                var notification = ViewData["Notification"] = new NotificationModel
                {
                    Property = "The Role Was Updated SuccessFully",
                    notificationType = NotificationType.Success

                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);
                return RedirectToPage("/RoleManagement/RoleList");
            }
        }
        public async  Task<IActionResult> OnPostDelete(string roleId)
        {
            IdentityRole = await _db.Roles.FirstOrDefaultAsync(x => x.Id == roleId) ?? new IdentityRole();
            var userRolesForThisrole = _db.UserRoles.Where(u => u.RoleId == roleId).Count();  
            if (userRolesForThisrole > 0)
            {
                {
                    {
                        {
                            {
                                {
                                    {
                                        {
                                            {
                                                {
                                                    {
                                                        {
                                                            {
                                                                {
                                                                    {
                                                                        {
                                                                            {
                                                                                {
                                                                                    {
                                                                                        {
                                                                                            {
                                                                                                {
                                                                                                    {
                                                                                                        {
                                                                                                            {
                                                                                                                {
                                                                                                                    {
                                                                                                                        {
                                                                                                                            {
                                                                                                                                {
                                                                                                                                    {
                                                                                                                                        {
                                                                                                                                            {
                                                                                                                                                {
                                                                                                                                                    {
                                                                                                                                                        {
                                                                                                                                                            {
                                                                                                                                                                {
                                                                                                                                                                    {
                                                                                                                                                                        {
                                                                                                                                                                            {
                                                                                                                                                                                {
                                                                                                                                                                                    var notification = ViewData["Notification"] = new NotificationModel
                                                                                                                                                                                    {
                                                                                                                                                                                        Property = "▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ Sorry Man But You can Not Delete This Role since there are users assigned to this role ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄",
                                                                                                                                                                                        notificationType = NotificationType.Failure
                                                                                                                                                                                    };
                                                                                                                                                                                    TempData["Notification"] = JsonSerializer.Serialize(notification);
                                                                                                                                                                                }
                                                                                                                                                                            }
                                                                                                                                                                        }
                                                                                                                                                                    }
                                                                                                                                                                }
                                                                                                                                                            }
                                                                                                                                                        }
                                                                                                                                                    }
                                                                                                                                                }
                                                                                                                                            }
                                                                                                                                        }
                                                                                                                                    }
                                                                                                                                }
                                                                                                                            }
                                                                                                                        }
                                                                                                                    }
                                                                                                                }
                                                                                                            }
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            var result = await roleManager.DeleteAsync(IdentityRole);

            if (result.Succeeded)
            {
                var notification = ViewData["Notification"] = new NotificationModel
                {
                    Property = "The Role Was Deleted Man",
                    notificationType = NotificationType.Success

                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);
                return RedirectToPage("/RoleManagement/RoleList");
            }

            else
            {
                var notification =  ViewData["Notification"] = new NotificationModel
                {
                    Property = "Sorry Man But Smth Went Wrong",
                    notificationType = NotificationType.Failure
                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);

                return RedirectToPage("/RoleManagement/RoleList");
            }

        }
    }
}
