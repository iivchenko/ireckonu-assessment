using AutoMapper;
using Ireckonu.Application.Commands.UploadFile;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ireckonu.FileUploadHost.Files
{
    [Route("api/files")]
    [ApiController]
    public sealed class FilesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public FilesController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<string> UploadFile(IFormFile file)
        {
            var command = _mapper.Map<UploadFileCommand>(file);

            var response = await _mediator.Send(command);

            return response.TargetFileName;
        }
    }
}
