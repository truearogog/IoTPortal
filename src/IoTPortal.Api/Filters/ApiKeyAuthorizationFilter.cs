using IoTPortal.Core.Services;
using IoTPortal.Framework.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IoTPortal.Api.Filters
{
    public class ApiKeyAuthorizationFilter(IApiKeyValidationService apiKeyValidationService) : IAsyncAuthorizationFilter
    {
        private readonly IApiKeyValidationService _apiKeyValidationService = apiKeyValidationService;

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var request = context.HttpContext.Request;
            var apiKey = request.Headers[HttpConstants.ApiKeyHeaderName].ToString();
            var deviceId = Guid.TryParse(request.Headers[HttpConstants.DeviceIdHeaderName], out var id) ? id : Guid.Empty;

            if (string.IsNullOrEmpty(apiKey) || deviceId == Guid.Empty)
            {
                context.Result = new BadRequestResult();
                return;
            }

            if (await _apiKeyValidationService.Validate(apiKey, deviceId) == false)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
