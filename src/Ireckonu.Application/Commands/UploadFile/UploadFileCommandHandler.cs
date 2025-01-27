﻿using Ireckonu.Application.Services.FileStorage;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Ireckonu.Application.Services.Events;
using Ireckonu.Application.Events;

namespace Ireckonu.Application.Commands.UploadFile
{
    public sealed class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, UploadFileCommandResponse>
    {
        private readonly ITemporaryStorage _storage;
        private readonly IEventService _eventService;
        private readonly ILogger _logger;

        public UploadFileCommandHandler(
            ITemporaryStorage storage,
            IEventService eventService,
            ILogger<UploadFileCommandHandler> logger)
        {
            _storage = storage;
            _eventService = eventService;
            _logger = logger;
        }

        public async Task<UploadFileCommandResponse> Handle(UploadFileCommand command, CancellationToken cancellationToken)
        {
            var name = $"{Guid.NewGuid()}-{command.SourceFileName}";

            _logger.LogInformation($"Start file '{command.SourceFileName}' upload into storage as '{name}' at {DateTime.UtcNow}");

            using (var writer = _storage.CreateFile(name))
            {
                await writer.WriteAsync(command.Content);
            }

            await _eventService.PublishAsync(new TemporaryFileUploadedEvent(name));

            _logger.LogInformation($"Finish file '{command.SourceFileName}' upload into storage as '{name}' at {DateTime.UtcNow}");

            return new UploadFileCommandResponse
            {
                TargetFileName = name
            };
        }
    }
}
