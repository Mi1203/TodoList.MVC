using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Domain.Entities;

namespace TodoList.Domain.Repositories
{
    public class TaskRepository : GenericRepository<TaskApp>
    {
        public TaskRepository(TodoListContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TaskApp>> GetTasksByUserIdAsync(int userId)
        {
            return await _context.Tasks.Where(t => t.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<TaskApp>> GetOverdueTasksAsync()
        {
            return await _context.Tasks.Where(t => t.ExpiredDate < DateTime.Now).ToListAsync();
        }
    }
}
