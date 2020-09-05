using Ireckonu.Application.Commands.UploadFile;
using Ireckonu.Application.Services.FileStorage;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
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
 
        [SetUp]
        public void Setup()
        {
            _fileStorage = new Mock<IFileStorage>();
            _fileFactory = new Mock<IFileFactory>();

            _handler = new UploadFileCommandHandler(_fileStorage.Object, _fileFactory.Object);
        }

        [Test]
        public async Task Handle_AllConditionsMet_Success()
        {
            // Arrange
            var content = new MemoryStream();
            var writer = new Mock<IFileWriter>();

            var command = new UploadFileCommand
            {
                SourceFileName = "File",
                Content = content
            };

            _fileStorage
                .Setup(x => x.CreateFile(It.IsAny<string>()))
                .Returns(writer.Object);

            // Act 
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(response.TargetFileName, Is.Not.Null);

            _fileFactory.Verify(x => x.CreateReader(content), Times.Once);
            _fileStorage.Verify(x => x.CreateFile(It.IsAny<string>()), Times.Once);

            writer.Verify(x => x.WriteAsync(It.IsAny<IEnumerable<string>>()), Times.Once);
        }
    }
}
