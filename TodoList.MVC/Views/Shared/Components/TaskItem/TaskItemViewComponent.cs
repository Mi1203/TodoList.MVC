using Microsoft.AspNetCore.Mvc;
using TodoList.Domain.Entities;

namespace TodoList.MVC.ViewComponents
{
    public class TaskItemViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(TaskApp task)
        {
            return View(task);
        }
    }
}
