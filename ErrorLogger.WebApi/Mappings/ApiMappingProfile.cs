using AutoMapper;
using ErrorLogger.Application.DTOs;
using ErrorLogger.Domain.Entities;


namespace ErrorLogger.WebApi.Mappings
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<UpdateErrorStatusDto, Error>()
                .ForMember(dest => dest.Status, opt => 
                    opt.MapFrom(src => ParseErrorStatus(src.Status)))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Message, opt => opt.Ignore())
                .ForMember(dest => dest.Source, opt => opt.Ignore())
                .ForMember(dest => dest.StatusCode, opt => opt.Ignore())
                .ForMember(dest => dest.Timestamp, opt => opt.Ignore());
        }
        
        private static ErrorStatus ParseErrorStatus(string status)
        {
            if (Enum.TryParse<ErrorStatus>(status, true, out var result))
            {
                return result;
            }

            return ErrorStatus.New;
        }
    }
}

