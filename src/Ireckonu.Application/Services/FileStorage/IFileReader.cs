using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ireckonu.Application.Services.FileStorage
{
    public interface IFileReader : IDisposable
    {
        IAsyncEnumerable<string> ReadAsync();
    }
}
