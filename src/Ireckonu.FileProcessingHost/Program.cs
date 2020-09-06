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

            SeedDb(host);

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

        private static void SeedDb(IHost host)
        {
            using (var serviceScope = host.Services.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<IreckonuContext>();
                if (context.Database.EnsureCreated())
                {
                    context.Articles.Add(new Article { Code = "2", Name = "Broek" });
                    context.Articles.Add(new Article { Code = "3", Name = "Kniebroek Jorge" });
                    context.Articles.Add(new Article { Code = "4", Name = "Jeans" });
                    context.Articles.Add(new Article { Code = "5", Name = "Jeans Willy" });
                    context.Articles.Add(new Article { Code = "6", Name = "Kniebroek Maria" });
                    context.Articles.Add(new Article { Code = "7", Name = "Top Wilma" });
                    context.Articles.Add(new Article { Code = "8", Name = "Top Annie" });
                    context.Articles.Add(new Article { Code = "9", Name = "Top Bill" });
                    context.Articles.Add(new Article { Code = "12", Name = "Steve Irwin" });
                    context.Articles.Add(new Article { Code = "15", Name = "Jeans Willy Boys" });
                    context.Articles.Add(new Article { Code = "0822801A", Name = "Short Billy & Bobble" });
                    context.Articles.Add(new Article { Code = "21", Name = "Jacket" });
                    context.Articles.Add(new Article { Code = "23", Name = "Test" });

                    context.Audiences.Add(new Audience { Name = "baby" });
                    context.Audiences.Add(new Audience { Name = "boy" });
                    context.Audiences.Add(new Audience { Name = "NOINDEX" });

                    context.Currencies.Add(new Currency { Name = "EUR" });

                    context.SaveChanges();
                }                
            }
        }
    }
}
