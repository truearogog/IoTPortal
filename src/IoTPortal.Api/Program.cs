using IoTPortal.Api.Filters;
using IoTPortal.Data.EF.Extensions;
using IoTPortal.Data.EF.SQLServer;
using IoTPortal.Identity.Extensions;
using IoTPortal.Identity.SQLServer;
using IoTPortal.Services.Extensions;
using Microsoft.EntityFrameworkCore;

namespace IoTPortal.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddMemoryCache();

            builder.Services.AddServices();
            builder.Services.AddAppEF<SQLServerAppDb>(options
                => options.UseSqlServer(builder.Configuration.GetConnectionString("AppDb") ?? throw new InvalidOperationException("Connection string 'AppDb' not found.")));
            builder.Services.AddIdentityEF<SQLServerIdentityDb>(options
                => options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityDb") ?? throw new InvalidOperationException("Connection string 'IdentityDb' not found.")));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<ApiKeyAuthorizationFilter>();
            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
