using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ireckonu.Application.Services.FileStorage
{
    public interface IFileReader : IDisposable
    {
        Task<IEnumerable<string>> ReadAsync();
    }
}
