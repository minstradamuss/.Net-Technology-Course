using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChatBook.Entities;
using ChatBook.DB;
using System.Data.Entity;
using System.Linq;
using ChatBook.DataAccess;
using System.IO;

namespace ChatBook.Tests
{
    [TestClass]
    public class UserTests
    {
        private const string TestDbPath = "ChatBook_Test.db";
        private const string TestDbConnection = "Data Source=ChatBook_Test.db";

        [TestInitialize]
        public void Init()
        {
            // Удаляем старую тестовую базу
            if (File.Exists(TestDbPath))
                File.Delete(TestDbPath);

            // Устанавливаем инициализатор с сид-данными
            Database.SetInitializer(new DbInitializer());

            using (var context = new ApplicationDbContext(TestDbConnection))
            {
                context.Database.Initialize(force: true);
            }
        }

        [TestMethod]
        public void Test_User_Is_Seeded()
        {
            using (var context = new ApplicationDbContext(TestDbConnection))
            {
                var user = context.Users.FirstOrDefault(u => u.Nickname == "seeduser");

                Assert.IsNotNull(user, "Сид-юзер не найден");
                Assert.AreEqual("Seed", user.FirstName);
            }
        }

        [TestMethod]
        public void Test_Seeded_Book_Exists()
        {
            using (var context = new ApplicationDbContext(TestDbConnection))
            {
                var book = context.Books.FirstOrDefault(b => b.Title == "Seed Book");

                Assert.IsNotNull(book, "Сид-книга не найдена");
                Assert.AreEqual(5, book.Rating);
            }
        }
    }
}
