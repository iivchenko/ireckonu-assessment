using Ireckonu.Application.Services.FileStorage;
using System.IO;

namespace Ireckonu.Infrastructure.Services.FileStorage
{
    public sealed class FileSystemFileFactory : IFileFactory
    {
        public IFileReader CreateReader(Stream stream)
        {
            return new FileSystemFileReader(stream);
        }
    }
}
