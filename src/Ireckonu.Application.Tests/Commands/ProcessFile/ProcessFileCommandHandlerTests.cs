using Ireckonu.Application.Commands.ProcessFile;
using Ireckonu.Application.Domain.Common;
using Ireckonu.Application.Domain.ProductAggregate;
using Ireckonu.Application.Services.Converters;
using Ireckonu.Application.Services.FileStorage;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Ireckonu.Application.Tests.Commands.ProcessFile
{
    [TestFixture]
    public sealed class ProcessFileCommandHandlerTests
    {
        private ProcessFileCommandHandler _handler;

        private Mock<ITemporaryStorage> _temporaryStorage;
        private Mock<IMainStorage> _mainStorage;
        private Mock<IConverter<IFileReader, IAsyncEnumerable<Product>>> _fileToProductConverter;
        private Mock<IConverter<Product, IAsyncEnumerable<string>>> _productTofileConverter;
        private Mock<IRepository<Product, Guid>> _productRepository;
        private Mock<ILogger<ProcessFileCommandHandler>> _logger;

        [SetUp]
        public void Setup()
        {
            _temporaryStorage = new Mock<ITemporaryStorage>();
            _mainStorage = new Mock<IMainStorage>();
            _fileToProductConverter = new Mock<IConverter<IFileReader, IAsyncEnumerable<Product>>>();
            _productTofileConverter = new Mock<IConverter<Product, IAsyncEnumerable<string>>>();
            _productRepository = new Mock<IRepository<Product, Guid>>();
            _logger = new Mock<ILogger<ProcessFileCommandHandler>>();

            _handler = new ProcessFileCommandHandler(
                _temporaryStorage.Object,
                _mainStorage.Object,
                _fileToProductConverter.Object,
                _productTofileConverter.Object,
                _productRepository.Object,
                _logger.Object);
        }

        [Test]
        public async Task Handle_AllConditionsMet_Success()
        {
            // Arrange
            var temporaryFileName = "file-name";
            var product1 = new Product
            {
                Id = Guid.NewGuid()
            };

            var product2 = new Product
            {
                Id = Guid.NewGuid()
            };

            var command = new ProcessFileCommand
            {
                TemporaryFileName = temporaryFileName
            };

            var fileReader = new Mock<IFileReader>();
            var fileWriter = new Mock<IFileWriter>();

            fileReader
                .Setup(x => x.ReadAsync())
                .Returns(new[] { "item1", "item2" }.ToAsyncEnumerable());

            _temporaryStorage
                .Setup(x => x.OpenForRead(temporaryFileName))
                .Returns(fileReader.Object);

            _mainStorage
                .Setup(x => x.CreateFile(It.IsAny<string>()))
                .Returns(fileWriter.Object);

            _fileToProductConverter
                .Setup(x => x.Convert(fileReader.Object))
                .Returns(new[] { product1, product2 }.ToAsyncEnumerable());

            _productTofileConverter
                .Setup(x => x.Convert(product1))
                .Returns(AsyncEnumerable.Empty<string>());

            _productTofileConverter
                .Setup(x => x.Convert(product2))
                .Returns(AsyncEnumerable.Empty<string>());

            _productRepository
                .SetupSequence(x => x.FindAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(Enumerable.Empty<Product>())
                .ReturnsAsync(new[] { product2 });

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _temporaryStorage.Verify(x => x.OpenForRead(temporaryFileName), Times.Once);
            _fileToProductConverter.Verify(x => x.Convert(It.IsAny<IFileReader>()), Times.Once);
            _productRepository.Verify(x => x.FindAsync(It.IsAny<Expression<Func<Product, bool>>>()), Times.Exactly(2));
            _productRepository.Verify(x => x.CreateAsync(product1), Times.Once);
            _productRepository.Verify(x => x.UpdateAsync(product2), Times.Once);

            fileWriter.Verify(x => x.WriteAsync(It.IsAny<IAsyncEnumerable<string>>()), Times.Exactly(2));
        }
    }
}
