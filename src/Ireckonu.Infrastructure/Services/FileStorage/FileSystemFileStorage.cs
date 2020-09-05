using Ireckonu.Application.Services.FileStorage;
using System.IO;

namespace Ireckonu.Infrastructure.Services.FileStorage
{
    public sealed class FileSystemFileStorage : IFileStorage
    {
        private readonly string _root;

        public FileSystemFileStorage(string root)
        {
            _root = root;
        }

        public IFileWriter CreateFile(string path)
        {
            var stream = File.Create(Path.Combine(_root, path));

            return new FileSystemFileWriter(stream);
        }

        public IFileReader OpenForRead(string path)
        {
            var stream = File.OpenRead(Path.Combine(_root, path));

            return new FileSystemFileReader(stream);
        }
    }
}
