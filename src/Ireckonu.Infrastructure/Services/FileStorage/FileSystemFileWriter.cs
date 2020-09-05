using Ireckonu.Application.Services.FileStorage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ireckonu.Infrastructure.Services.FileStorage
{
    public sealed class FileSystemFileWriter : IFileWriter
    {
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public Task WriteAsync(IEnumerable<string> lines)
        {
            throw new System.NotImplementedException();
        }
    }
}
