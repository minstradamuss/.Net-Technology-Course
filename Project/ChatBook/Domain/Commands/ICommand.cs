using ChatBook.Domain.Interfaces;
using ChatBook.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ChatBook.Domain.Commands
{
    public interface ICommand
    {
        void Execute();
    }
}
