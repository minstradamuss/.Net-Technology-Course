using Xunit;
using ChatBook.UI.DI;
using Microsoft.Extensions.DependencyInjection;
using Autofac;
using ChatBook.Domain.Interfaces;
using ChatBook.Domain.Services;
using ChatBook.DataAccess.Repositories;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChatBook.Tests
{
    public class DiContainerTests
    {
        [Fact]
        public void MicrosoftDi_Resolves_IBookService()
        {
            var provider = ServiceProviderBuilder.Build();

            var bookService = provider.GetService<IBookService>();

            Assert.IsNotNull(bookService);
        }



        [Fact]
        public void MicrosoftDi_Respects_Lifetimes()
        {
            var services = new ServiceCollection();

            services.AddSingleton<SingletonFake>();
            services.AddTransient<TransientFake>();
            services.AddScoped<ScopedFake>();

            var provider = services.BuildServiceProvider();

            var singleton1 = provider.GetService<SingletonFake>();
            var singleton2 = provider.GetService<SingletonFake>();
            Assert.AreSame(singleton1, singleton2);

            var transient1 = provider.GetService<TransientFake>();
            var transient2 = provider.GetService<TransientFake>();
            Assert.AreNotSame(transient1, transient2);

            using (var scope1 = provider.CreateScope())
            {
                var scoped1 = scope1.ServiceProvider.GetService<ScopedFake>();
                var scoped2 = scope1.ServiceProvider.GetService<ScopedFake>();
                Assert.AreSame(scoped1, scoped2);

                using (var scope2 = provider.CreateScope())
                {
                    var scoped3 = scope2.ServiceProvider.GetService<ScopedFake>();
                    Assert.AreNotSame(scoped1, scoped3);
                }
            }

        }
    }

    public class SingletonFake { }
    public class TransientFake { }
    public class ScopedFake { }
}
