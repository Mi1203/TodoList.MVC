using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Domain.Entities;

namespace TodoList.Domain.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(TodoListContext context) : base(context)
        {
        }

        public async Task<User> GetUserByLoginAsync(string login)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Login == login);
        }
    }
}
