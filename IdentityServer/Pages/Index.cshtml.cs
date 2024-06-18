using IdentityServer.Notification;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace IdentityServer.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            var notification = (string)TempData["Notification"];
            if(notification != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<NotificationModel>(notification);
            }
            var user = await 
        }
    }
}
