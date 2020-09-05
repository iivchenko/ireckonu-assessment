using Ireckonu.Application.Services.FileStorage;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Ireckonu.Application.Commands.UploadFile
{
    public sealed class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, UploadFileCommandResponse>
    {
        private readonly IFileStorage _storage;
        private readonly IFileFactory _fileFactory;

        public UploadFileCommandHandler(IFileStorage storage, IFileFactory fileFactory)
        {
            _storage = storage;
            _fileFactory = fileFactory;
        }

        public Task<UploadFileCommandResponse> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            // Create reader for input file stream
            // Create target file writer
            // read/write files
            // dispose stuff
            // log stuff
            throw new System.NotImplementedException();
        }
    }
}
