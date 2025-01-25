using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using TodoList.Domain;
using TodoList.Domain.Entities;

namespace TodoList.Tests
{
    [TestFixture]
    public class TodoListContextTests
    {
        private TodoListContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<TodoListContext>()
                .UseInMemoryDatabase(databaseName: "TodoListTestDb")
                .Options;

            return new TodoListContext(options);
        }

        [Test]
        public void AddUser_ShouldAddUserToDatabase()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var user = new User("TestLogin", "TestPassword");

            // Act
            context.Users.Add(user);
            context.SaveChanges();

            // Assert
            var savedUser = context.Users.SingleOrDefault(u => u.Login == "TestLogin");
            Assert.IsNotNull(savedUser);
            Assert.AreEqual("TestLogin", savedUser.Login);
            Assert.AreEqual("TestPassword", savedUser.Password);
        }


        [Test]
        public void AddTask_ShouldAddTaskToDatabase()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var task = new TaskApp
            {
                Id = 1,
                Name = "Test Task",
                ExpiredDate = System.DateTime.Now.AddDays(1),
                IsCompleted = false
            };

            // Act
            context.Tasks.Add(task);
            context.SaveChanges();

            // Assert
            var savedTask = context.Tasks.SingleOrDefault(t => t.Id == 1);
            Assert.IsNotNull(savedTask);
            Assert.AreEqual("Test Task", savedTask.Name);
            Assert.IsFalse(savedTask.IsCompleted);
        }

        [Test]
        public void DeleteUser_ShouldRemoveUserFromDatabase()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var user = new User("TestLoginD", "TestPasswordD");
            context.Users.Add(user);
            context.SaveChanges();

            // Act
            context.Users.Remove(user);
            context.SaveChanges();

            // Assert
            var deletedUser = context.Users.SingleOrDefault(u => u.Login == "TestLoginD");
            Assert.IsNull(deletedUser);
        }


        [Test]
        public void GetAllTasks_ShouldReturnAllTasks()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            context.Tasks.AddRange(
                new TaskApp { Name = "Task 1", ExpiredDate = DateTime.Now },
                new TaskApp { Name = "Task 2", ExpiredDate = DateTime.Now.AddDays(1) }
            );
            context.SaveChanges();

            // Act
            var tasks = context.Tasks.ToList();

            // Assert
            Assert.IsTrue(tasks.Any(t => t.Name == "Task 1"));
            Assert.IsTrue(tasks.Any(t => t.Name == "Task 2"));
        }

    }
}
