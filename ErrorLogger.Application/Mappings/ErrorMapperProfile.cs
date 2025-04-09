using AutoMapper;
using ErrorLogger.Domain.Models;
using ErrorLogger.Application.Commands;
using ErrorLogger.Application.DTOs;

namespace ErrorLogger.Application.Mappings
{
    public class ErrorMapperProfile : Profile
    {
        public ErrorMapperProfile()
        {
            CreateMap<Error, ErrorDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
            
            CreateMap<Error, ErrorSummaryDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
            
            CreateMap<CreateErrorDto, Error>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => ErrorStatus.New))
                .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(_ => DateTime.UtcNow));
            
            CreateMap<LogErrorCommand, Error>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => ErrorStatus.New))
                .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(_ => DateTime.UtcNow));
        }
    }
}

