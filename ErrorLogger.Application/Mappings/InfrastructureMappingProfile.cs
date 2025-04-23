using AutoMapper;
using ErrorLogger.Domain.Entities;

namespace ErrorLogger.Infrastructure.Mappings
{
    public class InfrastructureMappingProfile : Profile
    {
        public InfrastructureMappingProfile()
        {
            CreateMap<ErrorEntity, Error>()
                .ForMember(dest => dest.Status, opt => 
                    opt.MapFrom(src => (ErrorStatus)src.StatusId));
            
            CreateMap<Error, ErrorEntity>()
                .ForMember(dest => dest.StatusId, opt => 
                    opt.MapFrom(src => (int)src.Status));
        }
    }
}
