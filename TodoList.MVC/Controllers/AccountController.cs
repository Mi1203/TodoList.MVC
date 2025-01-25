using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Security.Claims;
using TodoList.Domain;
using TodoList.Domain.Entities;
using TodoList.MVC.Models.Account;

namespace TodoList.MVC.Controllers
{

    public class AccountController : ToDoBaseController
    {
        private TodoListContext _context;
        public AccountController(TodoListContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> LoginAsync([Bind(Prefix = "l")] LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Index", new AccountViewModel { LoginViewModel = model });

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == model.Password);

            if (user is null)
            {
                ViewBag.Error = "Login/passsord are not correct";
                return View("Index", new AccountViewModel
                {
                    LoginViewModel = model
                });
            }
            await AuthenticateAsync(user);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> RegisterAsync([Bind(Prefix = "r")] RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Index", new AccountViewModel { RegistrationViewModel = model });

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == model.Login);

            if (user != null)
            {
                ViewBag.Error = "Login already existig";
                return View("Index", new AccountViewModel
                {
                    RegistrationViewModel = model
                });
            }
            
            user = new User(model.Login, model.Password);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            
            await AuthenticateAsync(user);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> logoutAsync() 
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        public IActionResult Index()
        {
            return View();
        }

        private async Task AuthenticateAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
            };
            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
