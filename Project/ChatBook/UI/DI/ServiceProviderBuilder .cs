using Microsoft.Extensions.DependencyInjection;
using ChatBook.Domain.Interfaces;
using ChatBook.Domain.Services;
using ChatBook.DataAccess.Repositories;
using ChatBook.UI.Forms;
using ChatBook.Domain.Repositories;

namespace ChatBook.UI.DI
{
    public static class ServiceProviderBuilder
    {
        public static ServiceProvider Build()
        {
            var services = new ServiceCollection();

            // Жизненные циклы
            services.AddSingleton<IBookRepository, BookRepository>();     // Singleton
            services.AddScoped<IBookService, BookService>();              // Scoped
            services.AddTransient<IChatService, ChatRepository>();        // Transient

            // WinForms (регистрация форм)
            services.AddTransient<LoginForm>();
            services.AddTransient<MainForm>();

            return services.BuildServiceProvider();
        }
    }
}
