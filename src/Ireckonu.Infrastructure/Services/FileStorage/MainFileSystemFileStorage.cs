using Ireckonu.Application.Services.FileStorage;

namespace Ireckonu.Infrastructure.Services.FileStorage
{
    public sealed class MainFileSystemFileStorage : IMainStorage
    {
        private readonly FileSystemFileStorage _storage; 

        public MainFileSystemFileStorage(string root)
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
