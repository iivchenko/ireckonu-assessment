using Ireckonu.Application.Services.FileStorage;

namespace Ireckonu.Infrastructure.Services.FileStorage
{
    public sealed class TemporaryFileSystemFileStorage : ITemporaryStorage
    {
        private readonly FileSystemFileStorage _storage;

        public TemporaryFileSystemFileStorage(string root)
        {
            _storage = new FileSystemFileStorage(root);
        }

        public IFileWriter CreateFile(string path)
        {
            return _storage.CreateFile(path);
        }

        public IFileReader OpenForRead(string path)
        {
            return _storage.OpenForRead(path);
        }
    }
}
