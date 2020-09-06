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
        private readonly IFileStorage _temporaryStorage;
        private readonly IConverter<IFileReader, IAsyncEnumerable<Product>> _converter;
        private readonly IRepository<Product, Guid> _productRepoitory;
        private readonly ILogger<ProcessFileCommandHandler> _logger;

        public ProcessFileCommandHandler(
            IFileStorage temporaryStorage,
            IConverter<IFileReader, IAsyncEnumerable<Product>> fileToProductConverter,
            IRepository<Product, Guid> productRepoitory,
            ILogger<ProcessFileCommandHandler> logger)
        {
            _temporaryStorage = temporaryStorage;
            _converter = fileToProductConverter;
            _productRepoitory = productRepoitory;
            _logger = logger;
        }

        public async Task<Unit> Handle(ProcessFileCommand command, CancellationToken cancellationToken)
        {
            using (var reader = _temporaryStorage.OpenForRead(command.TemporaryFileName))
            {
                await foreach(var item in _converter.Convert(reader))
                {
                    var existing = (await _productRepoitory.FindAsync(x => x.Key == item.Key)).FirstOrDefault();

                    if (existing == null)
                    {
                        item.Id = Guid.NewGuid();

                        await _productRepoitory.CreateAsync(item);

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

                        _logger.LogTrace($"Exiting product with id ('{existing.Id}') updated.");
                    }
                }
            }

            return Unit.Value;
        }
    }
}
