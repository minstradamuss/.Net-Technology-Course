using ChatBook.Domain.Interfaces;
using ChatBook.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBook.Domain.Commands
{
    public class AddBookCommand : ICommand
    {
        private readonly IBookRepository _repo;
        private readonly Book _book;
        private readonly string _nickname;

        public AddBookCommand(IBookRepository repo, Book book, string nickname)
        {
            _repo = repo;
            _book = book;
            _nickname = nickname;
        }

        public void Execute()
        {
            _repo.AddBook(_book, _nickname);
        }
    }
}
