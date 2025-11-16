using AutoMapper;
using MinhaApi.DTOs;
using MinhaApi.Entities;

namespace MinhaApi.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserDto, User>();
    }
}
