using IoTPortal.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IoTPortal.Web.Pages
{
    [Authorize]
    public abstract class AuthPageModelBase(UserManager<User> userManager) : PageModel
    {
        protected readonly UserManager<User> UserManager = userManager;
        protected string? UserId => UserManager.GetUserId(User);
    }
}
