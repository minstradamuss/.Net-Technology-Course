using Xunit;
using ChatBook.Domain.Interfaces;
using ChatBook.Domain.Repositories;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChatBook.Tests
{
    public class InjectionTypesTests
    {
        [Fact]
        public void ConstructorInjection_Works()
        {
            var repo = new DummyBookRepository();
            var service = new ConstructorInjectedService(repo);
            Assert.Equals(repo, service.Repository);
        }

        [Fact]
        public void MethodInjection_Works()
        {
            var service = new MethodInjectedService();
            var repo = new DummyBookRepository();
            service.Inject(repo);
            Assert.Equals(repo, service.Repository);
        }

        [Fact]
        public void PropertyInjection_Works()
        {
            var service = new PropertyInjectedService();
            var repo = new DummyBookRepository();
            service.Repository = repo;
            Assert.Equals(repo, service.Repository);
        }
    }

    public class DummyBookRepository : IBookRepository
    {
        public void Add(ChatBook.Domain.Models.Book book) { }
        public void Update(ChatBook.Domain.Models.Book book) { }
        public List<ChatBook.Domain.Models.Book> GetAll()
        {
            return new List<ChatBook.Domain.Models.Book>();
        }

    }

    public class ConstructorInjectedService
    {
        public IBookRepository Repository { get; }
        public ConstructorInjectedService(IBookRepository repository)
        {
            Repository = repository;
        }
    }

    public class MethodInjectedService
    {
        public IBookRepository Repository { get; private set; }

        public void Inject(IBookRepository repository)
        {
            Repository = repository;
        }
    }

    public class PropertyInjectedService
    {
        public IBookRepository Repository { get; set; }
    }
}
