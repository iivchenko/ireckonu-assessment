using Ireckonu.Application.Domain.Common;
using Ireckonu.Application.Domain.ProductAggregate;
using Ireckonu.Application.Services.Converters;
using Ireckonu.Application.Services.FileStorage;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ireckonu.Application.Commands.ProcessFile
{
    public sealed class ProcessFileCommandHandler : IRequestHandler<ProcessFileCommand, Unit>
    {
        private readonly ITemporaryStorage _temporaryStorage;
        private readonly IMainStorage _mainStorage;
        private readonly IConverter<IFileReader, IAsyncEnumerable<Product>> _fileToProductConverter;
        private readonly IConverter<Product, IAsyncEnumerable<string>> _productTofileConverter;
        private readonly IRepository<Product, Guid> _productRepoitory;
        private readonly ILogger<ProcessFileCommandHandler> _logger;

        public ProcessFileCommandHandler(
            ITemporaryStorage temporaryStorage,
            IMainStorage mainStorage,
            IConverter<IFileReader, IAsyncEnumerable<Product>> fileToProductConverter,
            IConverter<Product, IAsyncEnumerable<string>> productTofileConverter,
            IRepository<Product, Guid> productRepoitory,
            ILogger<ProcessFileCommandHandler> logger)
        {
            _temporaryStorage = temporaryStorage;
            _mainStorage = mainStorage;
            _fileToProductConverter = fileToProductConverter;
            _productTofileConverter = productTofileConverter;
            _productRepoitory = productRepoitory;
            _logger = logger;
        }

        public async Task<Unit> Handle(ProcessFileCommand command, CancellationToken cancellationToken)
        {
            using (var reader = _temporaryStorage.OpenForRead(command.TemporaryFileName))
            {
                await foreach(var item in _fileToProductConverter.Convert(reader))
                {
                    var existing = (await _productRepoitory.FindAsync(x => x.Key == item.Key)).FirstOrDefault();

                    if (existing == null)
                    {
                        item.Id = Guid.NewGuid();

                        await _productRepoitory.CreateAsync(item);

                        var file = $"{item.Id}-{DateTime.UtcNow.ToString("yyyy-MM-dd-HH-mm-ss")}";
                        using (var writer = _mainStorage.CreateFile(file))
                        {
                            await writer.WriteAsync(_productTofileConverter.Convert(item));
                        }

                        _logger.LogTrace($"New product with id ('{item.Id}') created.");
                    }
                    else
                    {
                        existing.ArticleId = item.ArticleId;
                        existing.AudienceId = item.AudienceId;
                        existing.Color = item.Color;
                        existing.DeliveryPeriod = item.DeliveryPeriod;
                        existing.Description = item.Description;
                        existing.DiscountPrice = item.DiscountPrice;
                        existing.Key = item.Key;
                        existing.Price = item.Price;
                        existing.Size = item.Size;

                        await _productRepoitory.UpdateAsync(existing);

                        var file = $"{existing.Id}-{DateTime.UtcNow.ToString("yyyy-MM-dd-HH-mm-ss")}";
                        using (var writer = _mainStorage.CreateFile(file))
                        {
                            await writer.WriteAsync(_productTofileConverter.Convert(existing));
                        }

                        _logger.LogTrace($"Exiting product with id ('{existing.Id}') updated.");
                    }
                }
            }

            return Unit.Value;
        }
    }
}
