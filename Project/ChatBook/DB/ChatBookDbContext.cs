using System.Data.Common;
using System.Data.SQLite;
using System.Data.SqlClient;
using ChatBook.Domain.Models;
using System.Data.Entity;

public class ChatBookDbContext : DbContext
{
    private readonly bool _isSqlServer;

    // Конструктор, принимающий DbConnection
    public ChatBookDbContext(DbConnection connection)
        : base(connection, true)
    {
        _isSqlServer = connection is SqlConnection;
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Friendship> Friendships { get; set; }
    public DbSet<Message> Messages { get; set; }
}
