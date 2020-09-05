using AutoMapper;
using Ireckonu.Application.Commands.UploadFile;
using Microsoft.AspNetCore.Http;

namespace Ireckonu.FileUploadHost
{
    public sealed class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<IFormFile, UploadFileCommand>()
                .ForMember(dest => dest.SourceFileName, opt => opt.MapFrom(src => src.FileName))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.OpenReadStream()));
        }
    }
}
