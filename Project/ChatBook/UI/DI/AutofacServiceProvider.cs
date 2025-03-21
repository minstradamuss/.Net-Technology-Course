using Autofac;
using Autofac.Configuration;
using Microsoft.Extensions.Configuration;
using System.IO;

using IContainer = System.ComponentModel.IContainer;

namespace ChatBook.UI.DI
{
    public static class AutofacServiceProvider
    {
        public static IContainer Build()
        {
            var config = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("autofac.json", optional: false, reloadOnChange: true)
                .Build();

            var builder = new ContainerBuilder();
            var module = new ConfigurationModule(config);
            builder.RegisterModule(module);

            return (IContainer)builder.Build();
        }
    }
}
