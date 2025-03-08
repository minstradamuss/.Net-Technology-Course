using ChatBook.Domain.Models;
using System.Collections.Generic;

namespace ChatBook.Domain.Services
{
    public interface IBookService
    {
        void AddBook(Book book);
        void UpdateBook(Book book);
        List<Book> GetBooks();
    }
}
