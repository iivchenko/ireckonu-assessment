using MediatR;

namespace Ireckonu.Application.Commands.ProcessFile
{
    public sealed class ProcessFileCommand : IRequest<Unit>
    {
        public string TemporaryFileName { get; set; }
    }
}
