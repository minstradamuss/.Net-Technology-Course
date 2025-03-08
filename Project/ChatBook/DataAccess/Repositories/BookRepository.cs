using System.Collections.Generic;
using ChatBook.Domain.Models;
using ChatBook.Domain.Repositories;

namespace ChatBook.DataAccess.Repositories
{
    public class BookRepository : IBookRepository
    {
        private List<Book> _books = new List<Book>();

        public void Add(Book book) => _books.Add(book);

        public void Update(Book book)
        {
            var existingBook = _books.Find(b => b.Title == book.Title);
            if (existingBook != null)
            {
                existingBook.Status = book.Status;
                existingBook.Rating = book.Rating;
                existingBook.Review = book.Review;
                existingBook.CoverImagePath = book.CoverImagePath;
            }
        }

        public List<Book> GetAll() => _books;
    }
}
