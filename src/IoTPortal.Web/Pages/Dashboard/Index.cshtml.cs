using IoTPortal.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace IoTPortal.Web.Pages.Dashboard
{
    [Authorize]
    public class IndexModel(UserManager<User> userManager) : AuthPageModelBase(userManager)
    {
        public void OnGet()
        {
        }
    }
}
