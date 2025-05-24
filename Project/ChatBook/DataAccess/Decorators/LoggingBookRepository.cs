using ChatBook.Domain.Interfaces;
using ChatBook.Entities;
using System.Collections.Generic;
using System;

namespace ChatBook.DataAccess.Decorators
{
    public class LoggingBookRepository : IBookRepository
    {
        private readonly IBookRepository _inner;

        public LoggingBookRepository(IBookRepository inner)
        {
            _inner = inner;
        }

        public bool AddBook(Book book, string username)
        {
            Console.WriteLine($"[LOG] Adding book: {book.Title} for user {username}");
            return _inner.AddBook(book, username);
        }

        public bool DeleteBook(int bookId)
        {
            Console.WriteLine($"[LOG] Deleting book ID: {bookId}");
            return _inner.DeleteBook(bookId);
        }

        public List<Book> GetUserBooks(string username) => _inner.GetUserBooks(username);
        public List<Book> GetReadBooks(string username) => _inner.GetReadBooks(username);
        public bool UpdateBook(Book book) => _inner.UpdateBook(book);
        public Book GetBookByUserAndTitle(int userId, string title) => _inner.GetBookByUserAndTitle(userId, title);
        public List<BookWithReview> SearchBooksWithReviews(string query) => _inner.SearchBooksWithReviews(query);
    }
}
