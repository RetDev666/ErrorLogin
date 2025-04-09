using AutoMapper;
using ErrorLogger.Domain.Entities;
using ErrorLogger.Domain.Models;

namespace ErrorLogger.Infrastructure.Mappings
{
    public class InfrastructureMappingProfile : Profile
    {
        public InfrastructureMappingProfile()
        {
            // Мапінг з сутності бази даних на доменну модель
            CreateMap<ErrorEntity, Error>()
                .ForMember(dest => dest.Status, opt => 
                    opt.MapFrom(src => (ErrorStatus)src.StatusId));
            
            // Мапінг з доменної моделі на сутність бази даних
            CreateMap<Error, ErrorEntity>()
                .ForMember(dest => dest.StatusId, opt => 
                    opt.MapFrom(src => (int)src.Status));
        }
    }
}
