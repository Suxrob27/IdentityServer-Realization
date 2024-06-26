using IdentityServer.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Pages.RoleManagement
{
    public class RoleListModel : PageModel
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDB _dB;
        public List<IdentityRole> Roles { get; set; }

        public RoleListModel(RoleManager<IdentityRole> roleManager, ApplicationDB dB)
        {
            this.roleManager = roleManager;
            _dB = dB;
        }
        public IActionResult OnGet()
        {
            Roles = _dB.Roles.ToList();
            return Page();
        }
    }
}
