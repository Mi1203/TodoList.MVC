using System.ComponentModel.DataAnnotations;
using TodoList.Domain.Entities;

namespace TodoList.MVC.Models.Home
{
    public class HomeViewModel
    {
        [Required(ErrorMessage = "This Filed is required")]
        public string? TaskName { get; set; }

        [Required(ErrorMessage = "This Filed is required")]
        public DateTime? DateTime { get; set; }

        public IEnumerable<TaskApp>? Tasks { get; set; }
    }
}
