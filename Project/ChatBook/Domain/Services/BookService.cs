using System;
using System.Collections.Generic;
using ChatBook.Domain.Models;
using ChatBook.Domain.Repositories;

namespace ChatBook.Domain.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
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
    }
}
