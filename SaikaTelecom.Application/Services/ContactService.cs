namespace SaikaTelecom.Application.Services;

public class ContactService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public ContactService(ApplicationDbContext dbContext, IMapper mapper) 
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    /// <summary>
    /// Get all contacts, access for admin and marketer
    /// </summary>
    /// <returns></returns>
    public async Task<BaseResult<List<ContactDto>>> GetAllContacts()
    {
        var contacts = await _dbContext.Contacts.ToListAsync();
        if (contacts == null)
            return new BaseResult<List<ContactDto>>() { ErrorMessage = "No contacts found." };

        var allContacts = _mapper.Map<List<ContactDto>>(contacts);
        return new BaseResult<List<ContactDto>>() { Data = allContacts };
    }

    /// <summary>
    /// Get lead contacts, access for seller
    /// </summary>
    /// <returns></returns>
    public async Task<BaseResult<List<ContactDto>>> GetLeadContacts()
    {
        var contacts = await _dbContext.Contacts.Where(c => c.Status == ContactStatus.Lead).ToListAsync();
        if (contacts == null)
            return new BaseResult<List<ContactDto>>() { ErrorMessage = "No lead contacts found." };

        var allContacts = _mapper.Map<List<ContactDto>>(contacts);
        return new BaseResult<List<ContactDto>>() { Data = allContacts };
    }

    /// <summary>
    /// Create contact by using CreateContactDto
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<BaseResult<ContactDto>> CreateContact(CreateContactDto dto)
    {
        if (dto == null)
            return new BaseResult<ContactDto>() { ErrorMessage = "Contact data is null." };

        var newContact = _mapper.Map<Contact>(dto);

        await _dbContext.Contacts.AddAsync(newContact);
        await _dbContext.SaveChangesAsync();

        var contact = _mapper.Map<ContactDto>(newContact);

        return new BaseResult<ContactDto>() { Data = contact };
    }

    /// <summary>
    /// Update contact, finding by using id and change by using UpdateContactDto
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<BaseResult<ContactDto>> UpdateContact(long id, UpdateContactDto dto)
    {
        if (dto == null)
            return new BaseResult<ContactDto>() { ErrorMessage = "Update data is null." };

        var existingContact = await _dbContext.Contacts.FindAsync(id);
        if (existingContact == null)
            return new BaseResult<ContactDto>() { ErrorMessage = "Contact not found." };

        _mapper.Map(dto, existingContact);
        await _dbContext.SaveChangesAsync();

        var contactDto = _mapper.Map<ContactDto>(existingContact);

        return new BaseResult<ContactDto>() { Data = contactDto };
    }

    /// <summary>
    /// Change contact status, finding by using id and change by using UpdateStatusInContactDto
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<BaseResult<ContactDto>> ChangeContactStatus(long id, UpdateStatusInContactDto dto)
    {
        if (dto == null)
            return new BaseResult<ContactDto>() { ErrorMessage = "Status update data is null." };

        var contact = await _dbContext.Contacts.FindAsync(id);
        if (contact == null)
            return new BaseResult<ContactDto>() { ErrorMessage = "Contact not found." };

        contact.Status = dto.Status;
        await _dbContext.SaveChangesAsync();

        var updatedContact = _mapper.Map<ContactDto>(contact);
        return new BaseResult<ContactDto>() { Data = updatedContact };
    }
}
