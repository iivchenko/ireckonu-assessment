using Ireckonu.Application.Services.FileStorage;

namespace Ireckonu.Infrastructure.Services.FileStorage
{
    public sealed class FileSystemFileStorage : IFileStorage
    {
        public IFileWriter CreateFile(string path)
        {
            throw new System.NotImplementedException();
        }

        public IFileReader OpenForRead(string path)
        {
            throw new System.NotImplementedException();
        }
    }
}
