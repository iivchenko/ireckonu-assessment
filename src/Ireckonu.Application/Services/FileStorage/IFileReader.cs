using System;
using System.Collections.Generic;

namespace Ireckonu.Application.Services.FileStorage
{
    public interface IFileReader : IDisposable
    {
        IAsyncEnumerable<string> ReadAsync();
    }
}
