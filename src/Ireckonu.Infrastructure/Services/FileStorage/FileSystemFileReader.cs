using Ireckonu.Application.Services.FileStorage;
using System.Collections.Generic;
using System.IO;

namespace Ireckonu.Infrastructure.Services.FileStorage
{
    public sealed class FileSystemFileReader : IFileReader
    {
        private readonly Stream _stream;

        internal FileSystemFileReader(Stream stream)
        {
            _stream = stream;
        }

        public void Dispose()
        {
            _stream.Dispose();
        }

        public async IAsyncEnumerable<string> ReadAsync()
        {
            using (var reader = new StreamReader(_stream))
            {
                while(true)
                {
                    var item = await reader.ReadLineAsync();

                    if (item == null)
                    {
                        break;
                    }

                    yield return item;
                }
            }
        }
    }
}
