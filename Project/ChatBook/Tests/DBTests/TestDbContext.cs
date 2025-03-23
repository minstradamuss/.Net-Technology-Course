using ChatBook.DataAccess;
using ChatBook.DB;

namespace ChatBook.Tests
{
    public class TestDbContext : ApplicationDbContext
    {
        public TestDbContext()
            : base("Data Source=ChatBook_Test.db")
        {
        }
    }
}
