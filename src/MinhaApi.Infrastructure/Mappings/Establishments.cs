using AutoMapper;
using MinhaApi.DTOs;
using MinhaApi.Entities;

namespace MinhaApi.Mappings;

public class EstablishmentProfile : Profile
{
    public EstablishmentProfile()
    {
        CreateMap<CreateEstablishmentDto, Establishments>();
        CreateMap<UpdateEstablishmentDto, Establishments>()
            .ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<Establishments, EstablishmentDto>();
    }
}
