using ChatBook.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBook.Domain.Interfaces
{
    using ChatBook.Entities;
    using System.Collections.Generic;
    public interface IBookRepository
    {
        bool AddBook(Book book, string username);
        bool UpdateBook(Book book);
        bool DeleteBook(int bookId);
        List<Book> GetUserBooks(string username);
        List<Book> GetReadBooks(string username);
        Book GetBookByUserAndTitle(int userId, string bookTitle);
        List<BookWithReview> SearchBooksWithReviews(string titleQuery);
    }
}
