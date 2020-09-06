using AutoMapper;
using EasyNetQ;
using Ireckonu.Application.Domain.ArticleAggregate;
using Ireckonu.Application.Domain.AudienceAggregate;
using Ireckonu.Application.Domain.Common;
using Ireckonu.Application.Domain.CurrencyAggregate;
using Ireckonu.Application.Domain.ProductAggregate;
using Ireckonu.Application.Services.Converters;
using Ireckonu.Application.Services.Events;
using Ireckonu.Application.Services.FileStorage;
using Ireckonu.Infrastructure.Domain;
using Ireckonu.Infrastructure.Domain.ArticleAggregate;
using Ireckonu.Infrastructure.Domain.AudienceAggregate;
using Ireckonu.Infrastructure.Domain.CurrencyAggregate;
using Ireckonu.Infrastructure.Domain.ProductAggregate;
using Ireckonu.Infrastructure.Services.Converters;
using Ireckonu.Infrastructure.Services.Events;
using Ireckonu.Infrastructure.Services.FileStorage;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace Ireckonu.FileUploadHost
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IFileFactory, FileSystemFileFactory>();
            services.AddScoped<IFileStorage, FileSystemFileStorage>(x => new FileSystemFileStorage(Configuration.GetValue<string>("Storage:Temporary")));

            services.AddScoped<IEventService>(x => new RabbitMqEventService(RabbitHutch.CreateBus(Configuration.GetConnectionString("RabbitMq"))));

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
                                Configuration.GetConnectionString("MsSqlServer"));
                    });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Ireckonu File Upload API"
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ireckonu File Upload Api V1");
                c.DocumentTitle = "Ireckonu File Upload";
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
