using IoTPortal.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IoTPortal.Web.Controllers.Api
{
    public class UserController(UserManager<User> userManager) : AuthControllerBase(userManager)
    {
        [HttpGet("find/username")]
        public async Task<IActionResult> GetFindUsername(string search)
        {
            return await Execute(async context =>
            {
                var usernames = await UserManager.Users
                    .Where(x => !string.IsNullOrEmpty(x.UserName) && x.UserName.Contains(search))
                    .Select(x => x.UserName!)
                    .Take(5)
                    .ToListAsync();
                return usernames;
            });
        }
    }
}
