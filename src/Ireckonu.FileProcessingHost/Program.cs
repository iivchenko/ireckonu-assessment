using AutoMapper;
using Ireckonu.Application.Commands.ProcessFile;
using Ireckonu.FileProcessingHost.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;

namespace Ireckonu.FileProcessingHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
                    services.AddMediatR(typeof(ProcessFileCommand).GetTypeInfo().Assembly);

                    services.AddHostedService<RabbitMqBackgroundService>();
                });
    }
}
