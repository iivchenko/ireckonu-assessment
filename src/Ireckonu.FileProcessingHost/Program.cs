using AutoMapper;
using Ireckonu.Application.Commands.ProcessFile;
using Ireckonu.Application.Domain.ArticleAggregate;
using Ireckonu.Application.Domain.AudienceAggregate;
using Ireckonu.Application.Domain.Common;
using Ireckonu.Application.Domain.CurrencyAggregate;
using Ireckonu.Application.Domain.ProductAggregate;
using Ireckonu.Application.Services.Converters;
using Ireckonu.Application.Services.FileStorage;
using Ireckonu.FileProcessingHost.Services;
using Ireckonu.Infrastructure.Domain;
using Ireckonu.Infrastructure.Domain.ArticleAggregate;
using Ireckonu.Infrastructure.Domain.AudienceAggregate;
using Ireckonu.Infrastructure.Domain.CurrencyAggregate;
using Ireckonu.Infrastructure.Domain.ProductAggregate;
using Ireckonu.Infrastructure.Services.Converters;
using Ireckonu.Infrastructure.Services.FileStorage;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Ireckonu.FileProcessingHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var serviceScope = host.Services.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<IreckonuContext>();
                context.Database.EnsureCreated();

                context.SaveChanges();
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddScoped<IFileStorage, FileSystemFileStorage>(x => new FileSystemFileStorage(hostContext.Configuration.GetValue<string>("Storage:Temporary")));

                    services.AddScoped<IConverter<IFileReader, IAsyncEnumerable<Product>>, CvsToProductConverter>();

                    services.AddScoped<IRepository<Product, Guid>, EfProductRepository>();
                    services.AddScoped<IRepository<Article, Guid>, EfArticleRepository>();
                    services.AddScoped<IRepository<Audience, Guid>, EfAudienceRepository>();
                    services.AddScoped<IRepository<Currency, Guid>, EfCurrencyRepository>();

                    services
                        .AddDbContext<IreckonuContext>(
                            options =>
                            {
                                options
                                    .UseSqlServer(
                                        hostContext.Configuration.GetConnectionString("MsSqlServer"),
                                        sqlOptions => sqlOptions.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name));
                            });

                    services.AddHostedService<RabbitMqBackgroundService>();

                    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
                    services.AddMediatR(typeof(ProcessFileCommand).GetTypeInfo().Assembly);
                });
    }
}
