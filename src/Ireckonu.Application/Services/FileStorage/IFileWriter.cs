using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Ireckonu.Application.Services.FileStorage
{
    public interface IFileWriter : IDisposable
    {
        Task WriteAsync(IAsyncEnumerable<string> lines);

        Task WriteAsync(Stream source);
    }
}
