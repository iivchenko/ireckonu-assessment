using MediatR;
using System.IO;

namespace Ireckonu.Application.Commands.UploadFile
{
    public sealed class UploadFileCommand : IRequest<UploadFileCommandResponse>
    {
        public string SourceFileName { get; set; }

        public Stream Content { get; set; }
    }
}
