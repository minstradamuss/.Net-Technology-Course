using System;
using System.Collections.Generic;
using ChatBook.Entities;

namespace ChatBook.Domain.Services
{
    public class BookService : IBookService
    {
        private readonly IBookService _bookRepository;

        public BookService(IBookService bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public void AddBook(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Title))
                throw new ArgumentException("Название книги не может быть пустым");

            _bookRepository.Add(book);
        }

        public void UpdateBook(Book book)
        {
            _bookRepository.Update(book);
        }

        public List<Book> GetBooks()
        {
            return _bookRepository.GetAll();
        }

        public void Add(Book book)
        {
            throw new NotImplementedException();
        }

        public void Update(Book book)
        {
            throw new NotImplementedException();
        }

        public List<Book> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
