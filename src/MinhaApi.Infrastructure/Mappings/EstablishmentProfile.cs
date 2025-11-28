using AutoMapper;
using MinhaApi.Domain.Entities;
using MinhaApi.DTOs.Establishments;

namespace MinhaApi.Infrastructure.Mappings;

public class EstablishmentProfile : Profile
{
    public EstablishmentProfile()
    {
        CreateMap<CreateEstablishmentDto, Establishment>();
        CreateMap<UpdateEstablishmentDto, Establishment>()
            .ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<Establishment, EstablishmentResponseDto>();
    }
}
