using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using PrasTestJobData;
using PrasTestJobServices.Abstract;
using PrasTestJobServices.Implementations;

namespace PrasTestJobWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<PrasTestJobContext>(opt =>
            {
                opt.UseSqlite(builder.Configuration.GetConnectionString("Alternative"));
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.LoginPath = "/admin/Authentication/Login";
                    options.AccessDeniedPath = "/admin/Authentication/AccessDenied";
                    options.LogoutPath = "/admin/Authentication/Logout";
                });

            builder.Services.AddScoped<IUserServices, UserServices>();
            builder.Services.AddScoped<INewsServices, NewsServices>();
            builder.Services.AddSingleton<IPasswordHashing, PBKDF2PasswordHashing>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Default", policy =>
                {
                    policy.WithOrigins("http://localhost:4200", "https://localhost:4200")
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }


            app.UseHttpsRedirection();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(app.Environment.WebRootPath, "browser")),
                RequestPath = ""
            });


            app.UseRouting();

            app.UseCors("Default");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.MapControllerRoute(
                name: "admin",
                pattern: "admin/{controller=AdminPanel}/{action=Index}/{id?}");


            app.MapFallbackToFile(/*"/app/{*path}",*/ "browser/index.html");

            app.Run();
        }
    }
}
