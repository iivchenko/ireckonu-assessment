using Ireckonu.Application.Domain.Common;
using Ireckonu.Application.Domain.ProductAggregate;
using Ireckonu.Application.Services.Converters;
using Ireckonu.Application.Services.FileStorage;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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

        public Task<Unit> Handle(ProcessFileCommand command, CancellationToken cancellationToken)
        {
            // Open file from temporary storage for read
            // async item read
            // async item convert to logical model
            // asycn item write to repository
            // dispose stuff
            // log stuff

            throw new System.NotImplementedException();
        }
    }
}
