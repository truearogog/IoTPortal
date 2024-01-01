using IoTPortal.Api.Filters;
using Microsoft.AspNetCore.Mvc;

namespace IoTPortal.Api.Attributes
{
    public class ApiKeyAttribute : ServiceFilterAttribute
    {
        public ApiKeyAttribute() : base(typeof(ApiKeyAuthorizationFilter))
        {
        }
    }
}
