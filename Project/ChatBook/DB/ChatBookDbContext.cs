using System.Data.Common;
using System.Data.Entity;
using ChatBook.Domain.Models;

namespace ChatBook
{
    public class ChatBookDbContext : DbContext
    {
        public ChatBookDbContext(DbConnection connection)
            : base(connection, contextOwnsConnection: true) // Контекст управляет соединением
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
