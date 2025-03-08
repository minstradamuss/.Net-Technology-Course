using System.Collections.Generic;
using ChatBook.Domain.Models;

namespace ChatBook.Domain.Repositories
{
    public interface IBookRepository
    {
        void Add(Book book);
        void Update(Book book);
        List<Book> GetAll();
    }
}
