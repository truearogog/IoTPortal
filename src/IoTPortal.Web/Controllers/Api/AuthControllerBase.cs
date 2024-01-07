using IoTPortal.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IoTPortal.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthControllerBase(UserManager<User> userManager) : ControllerBase
    {
        protected readonly UserManager<User> UserManager = userManager;
        public string? UserId => UserManager.GetUserId(User);

        protected class ExecuteContext
        {
            public bool Authorized { get; set; }
        }

        protected async Task<IActionResult> Execute<T>(Func<ExecuteContext, Task<T>> action)
        {
            if (!User.Identity!.IsAuthenticated)
            {
                return Unauthorized();
            }

            try
            {
                var executeContext = new ExecuteContext
                {
                    Authorized = true
                };
                var result = await action(executeContext);

                if (!executeContext.Authorized)
                {
                    return Unauthorized();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
