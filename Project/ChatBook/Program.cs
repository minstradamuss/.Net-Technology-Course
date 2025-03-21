using System;
using System.Windows.Forms;
using ChatBook.UI.Forms;
using ChatBook.UI.DI;
using Microsoft.Extensions.DependencyInjection;
// или:
// using Autofac;

namespace ChatBook
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Microsoft.Extensions.DependencyInjection (Code-based)
            var serviceProvider = ServiceProviderBuilder.Build();
            var loginForm = serviceProvider.GetRequiredService<LoginForm>();
            Application.Run(loginForm);

            // или Autofac

            // var container = AutofacServiceProvider.Build();
            // using var scope = container.BeginLifetimeScope();
            // var loginForm = scope.Resolve<LoginForm>();
            // Application.Run(loginForm);
        }
    }
}
