using System.IO;

namespace Ireckonu.Application.Services.FileStorage
{
    public interface IFileFactory
    {
        IFileReader CreateReader(Stream stream);
    }
}
