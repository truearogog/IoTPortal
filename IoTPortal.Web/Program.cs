using IoTPortal.Data.EF.Extensions;
using IoTPortal.Data.EF.SQLServer;
using IoTPortal.Identity.Extensions;
using IoTPortal.Identity.Models;
using IoTPortal.Identity.SQLServer;
using Microsoft.EntityFrameworkCore;
namespace IoTPortal.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAppEF<SQLServerAppDb>(options
                => options.UseSqlServer(builder.Configuration.GetConnectionString("AppDb") ?? throw new InvalidOperationException("Connection string 'AppDb' not found.")));
            builder.Services.AddIdentityEF<SQLServerIdentityDb>(options
                => options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityDb") ?? throw new InvalidOperationException("Connection string 'IdentityDb' not found.")));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services
                .AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<SQLServerIdentityDb>();

            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}