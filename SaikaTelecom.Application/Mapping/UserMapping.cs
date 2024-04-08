namespace SaikaTelecom.Application.Mapping;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<User, UserResponse>().ReverseMap();

        CreateMap<User, UserGetDto>().ReverseMap();

        CreateMap<User, SignUpUserDto>().ReverseMap();
    }
}
