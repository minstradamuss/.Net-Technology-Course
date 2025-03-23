using System.Data.Entity;
using ChatBook.DataAccess;
using ChatBook.Entities;

namespace ChatBook.DB
{
    public class DbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var user = new User
            {
                Nickname = "seeduser",
                FirstName = "Seed",
                LastName = "User",
                Password = "1234"
            };

            context.Users.Add(user);
            context.SaveChanges();

            context.Books.Add(new Book
            {
                Title = "Seed Book",
                Status = "Прочитано",
                Rating = 5,
                Review = "Отличная книга!",
                UserId = user.Id
            });

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
