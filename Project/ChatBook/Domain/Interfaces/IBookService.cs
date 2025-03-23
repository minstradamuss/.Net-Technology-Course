using ChatBook.Entities;
using System.Collections.Generic;

namespace ChatBook.Domain.Services
{
    public interface IBookService
    {
        void AddBook(Book book);
        void UpdateBook(Book book);
        List<Book> GetBooks();
        void Add(Book book);
        void Update(Book book);
        List<Book> GetAll();
    }
}
