using CarRenting.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace CarRenting
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            // FIX 1: Register DbContext with DI using connection string from appsettings.json
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                // FIX 2: Session timeout was 10 seconds — completely non-functional
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.IOTimeout = TimeSpan.FromMinutes(60);
                options.Cookie.Name = "CarRenting";
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.Path = "/";
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            // FIX 3: UseSession must come BEFORE UseAuthorization, not after
            app.UseSession();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
