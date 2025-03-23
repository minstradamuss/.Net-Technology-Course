//// UserServiceTests.cs
//using ChatBook.DataAccess;
//using ChatBook.Domain.Models;
//using ChatBook.Services;
//using Microsoft.EntityFrameworkCore;
//using Xunit;

//namespace ChatBook.Tests
//{
//    public class UserServiceTests
//    {
//        [Fact]
//        public void Register_ShouldAddUser_WhenValidData()
//        {
//            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
//                .UseInMemoryDatabase(databaseName: "TestDb")
//                .Options;
//            var context = new ApplicationDbContext(options);
//            var userService = new UserService(context);

//            var user = new User { Nickname = "NewUser", FirstName = "John", LastName = "Doe" };

//            var result = userService.Register(user);

//            Assert.True(result);
//            Assert.Contains(user, context.Users);
//        }

//        [Fact]
//        public void GetUserByNickname_ShouldReturnUser_WhenExists()
//        {
//            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
//                .UseInMemoryDatabase(databaseName: "TestDb")
//                .Options;
//            var context = new ApplicationDbContext(options);
//            var userService = new UserService(context);

//            var user = new User { Nickname = "ExistingUser", FirstName = "Jane", LastName = "Doe" };
//            context.Users.Add(user);
//            context.SaveChanges();

//            var result = userService.GetUserByNickname("ExistingUser");

//            Assert.NotNull(result);
//            Assert.Equal("ExistingUser", result.Nickname);
//        }
//    }
//}
