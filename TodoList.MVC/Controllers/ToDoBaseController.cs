using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TodoList.MVC.Controllers
{
    public class ToDoBaseController : Controller
    {
        protected int CurrentUserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}
