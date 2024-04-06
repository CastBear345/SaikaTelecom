namespace SaikaTelecom.Application.Mapping;

public class LeadMapping : Profile
{
    public LeadMapping()
    {
        CreateMap<Lead, LeadResponse>().ReverseMap();

        CreateMap<Lead, LeadCreateDto>().ReverseMap();
    }
}
