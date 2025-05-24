using ChatBook.Domain.Interfaces;
using ChatBook.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBook.Domain.Commands
{
    public class DeleteBookCommand : ICommand
    {
        private readonly IBookRepository _repo;
        private readonly int _bookId;

        public DeleteBookCommand(IBookRepository repo, int bookId)
        {
            _repo = repo;
            _bookId = bookId;
        }

        public void Execute()
        {
            _repo.DeleteBook(_bookId);
        }
    }
}