﻿using Ireckonu.Application.Commands.UploadFile;
using Ireckonu.Application.Services.FileStorage;
using Moq;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Ireckonu.Application.Tests.Commands.UploadFile
{
    [TestFixture]
    public sealed class UploadFileCommandHandlerTests
    {
        private UploadFileCommandHandler _handler;

        private Mock<IFileStorage> _fileStorage;
        private Mock<IFileFactory> _fileFactory;
        private Mock<ILogger<UploadFileCommandHandler>> _logger;
 
        [SetUp]
        public void Setup()
        {
            _fileStorage = new Mock<IFileStorage>();
            _fileFactory = new Mock<IFileFactory>();
            _logger = new Mock<ILogger<UploadFileCommandHandler>>();

            _handler = new UploadFileCommandHandler(_fileStorage.Object, _fileFactory.Object, _logger.Object);
        }

        [Test]
        public async Task Handle_AllConditionsMet_Success()
        {
            // Arrange
            var content = new MemoryStream();
            var reader = new Mock<IFileReader>();
            var writer = new Mock<IFileWriter>();

            var command = new UploadFileCommand
            {
                SourceFileName = "File",
                Content = content
            };

            reader
                .Setup(x => x.ReadAsync())
                .ReturnsAsync(Enumerable.Empty<string>());

            _fileFactory
                .Setup(x => x.CreateReader(content))
                .Returns(reader.Object);

            _fileStorage
                .Setup(x => x.CreateFile(It.IsAny<string>()))
                .Returns(writer.Object);

            // Act 
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(response.TargetFileName, Is.Not.Null);

            _fileFactory.Verify(x => x.CreateReader(content), Times.Once);
            _fileStorage.Verify(x => x.CreateFile(It.IsAny<string>()), Times.Once);

            reader.Verify(x => x.ReadAsync(), Times.Once);
            writer.Verify(x => x.WriteAsync(It.IsAny<IEnumerable<string>>()), Times.Once);
        }
    }
}
