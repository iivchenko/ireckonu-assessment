using AutoMapper;
using Ireckonu.Application.Commands.ProcessFile;
using Ireckonu.Application.Events;

namespace Ireckonu.FileProcessingHost
{
    public sealed class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TemporaryFileUploadedEvent, ProcessFileCommand>()
              .ForMember(dest => dest.TemporaryFileName, opt => opt.MapFrom(src => src.File));
        }
    }
}
