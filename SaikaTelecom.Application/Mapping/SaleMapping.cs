namespace SaikaTelecom.Application.Mapping;

public class SaleMapping : Profile
{
    public SaleMapping()
    {
        CreateMap<Sale, SaleGetDto>().ReverseMap();

        CreateMap<Sale, SaleResponse>().ReverseMap();
    }
}
