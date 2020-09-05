using Ireckonu.Application.Services.FileStorage;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Ireckonu.Infrastructure.Services.FileStorage
{
    public sealed class FileSystemFileWriter : IFileWriter
    {
        private readonly Stream _stream;

        internal FileSystemFileWriter(Stream stream)
        {
            _stream = stream;
        }

        public void Dispose()
        {
            _stream.Dispose();
        }

        public async Task WriteAsync(IAsyncEnumerable<string> lines)
        {
            using (var writer = new StreamWriter(_stream))
            {
                await foreach(var line in lines)
                {
                    await writer.WriteAsync(line);
                }
            }
        }
    }
}
