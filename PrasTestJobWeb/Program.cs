using Microsoft.Extensions.FileProviders;

namespace PrasTestJobWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles( new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(app.Environment.WebRootPath, "browser")),
                RequestPath = ""
            });


            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "admin",
                pattern: "admin/{controller=Home}/{action=Index}/{id?}");

            app.MapControllers();

            app.MapFallbackToFile("browser/index.html");

            app.Run();
        }
    }
}
