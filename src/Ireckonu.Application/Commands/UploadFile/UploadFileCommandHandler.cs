using Ireckonu.Application.Services.FileStorage;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Ireckonu.Application.Commands.UploadFile
{
    public sealed class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, UploadFileCommandResponse>
    {
        private readonly IFileStorage _storage;
        private readonly IFileFactory _fileFactory;
        private readonly ILogger _logger;

        public UploadFileCommandHandler(
            IFileStorage storage,
            IFileFactory fileFactory,
            ILogger<UploadFileCommandHandler> logger)
        {
            _storage = storage;
            _fileFactory = fileFactory;
            _logger = logger;
        }

        public async Task<UploadFileCommandResponse> Handle(UploadFileCommand command, CancellationToken cancellationToken)
        {
            var name = $"{Guid.NewGuid()}-{command.SourceFileName}";

            _logger.LogInformation($"Start file '{command.SourceFileName}' upload into storage as '{name}'");

            using (var reader = _fileFactory.CreateReader(command.Content))
            using (var writer = _storage.CreateFile(name))
            {
                await writer.WriteAsync(reader.ReadAsync());
            }

            _logger.LogInformation($"Finish file '{command.SourceFileName}' upload into storage as '{name}'");

            return new UploadFileCommandResponse
            {
                TargetFileName = name
            };
        }
    }
}
