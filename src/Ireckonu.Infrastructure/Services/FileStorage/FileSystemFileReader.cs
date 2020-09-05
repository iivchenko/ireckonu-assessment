using Ireckonu.Application.Services.FileStorage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ireckonu.Infrastructure.Services.FileStorage
{
    public sealed class FileSystemFileReader : IFileReader
    {
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<string>> ReadAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
