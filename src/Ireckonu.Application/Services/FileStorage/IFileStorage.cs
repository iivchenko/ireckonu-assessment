namespace Ireckonu.Application.Services.FileStorage
{
    public interface IFileStorage
    {
        IFileReader OpenForRead(string path);

        IFileWriter CreateFile(string path);
    }
}
