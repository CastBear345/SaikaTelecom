using AutoMapper;
using SaikaTelecom.Domain.Contracts.UserDtos;
using SaikaTelecom.Domain.Entities;

namespace SaikaTelecom.Application.Mapping;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<User, UserDto>().ReverseMap();

        CreateMap<User, UserGetDto>()
            .ReverseMap();

        CreateMap<User, RegisterUserDto>()
            .ReverseMap();
    }
}
