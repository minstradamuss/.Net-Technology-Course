using ChatBook.Domain.Models;
using System.Collections.Generic;

namespace ChatBook.Domain.Interfaces
{
    public interface IBookService
    {
        List<Book> SearchBooks(string query);
    }
}
