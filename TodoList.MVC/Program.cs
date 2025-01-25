using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Net;
using TodoList.Domain;
using TodoList.MVC.CustomMiddleware;
using TodoList.Domain;
using TodoList.MVC.CustomMiddleware;

namespace ToDoList.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //var configurationString = builder.Configuration.GetConnectionString("Default");
            //builder.Services.AddDbContext<TodoListContext>(opt => opt.UseSqlServer(configurationString));

            builder.Services.AddDbContext<TodoListContext>(options => options.UseInMemoryDatabase("InMemoryDb"));
           
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => options.LoginPath = "/account");

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //add Custom middleware for redirect not 
            app.UseMiddleware<NotFoundMiddleware>();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
