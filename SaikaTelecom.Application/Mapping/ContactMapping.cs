namespace SaikaTelecom.Application.Mapping;

public class ContactMapping : Profile
{
    public ContactMapping()
    {
        CreateMap<Contact, ContactDto>().ReverseMap();

        CreateMap<Contact, CreateContactDto>().ReverseMap();

        CreateMap<Contact, UpdateContactDto>().ReverseMap();
    }
}
