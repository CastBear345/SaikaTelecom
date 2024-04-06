namespace SaikaTelecom.Application.Services;

public class LeadService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public LeadService(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<BaseResult<List<LeadResponse>>> GetSellerLeads(long sellerId)
    {
        var lead = await _dbContext.Leads
            .AsNoTracking()
            .Where(l => l.SellerId == sellerId)
            .ToListAsync();

        return lead == null || lead.Count <= 0
            ? new BaseResult<List<LeadResponse>>() { ErrorMessage = "No leads were found." }
            : new BaseResult<List<LeadResponse>>() { Data = _mapper.Map<List<LeadResponse>>(lead) };
    }

    public async Task<BaseResult<LeadResponse>> CreateLead(LeadCreateDto dto)
    {
        var doesLeadExist = await _dbContext.Leads
            .AsNoTracking()
            .AnyAsync(lead => lead.ContactId == dto.ContactId);

        if (doesLeadExist) return new BaseResult<LeadResponse> { ErrorMessage = "Lead already exists." };

        var newLead = _mapper.Map<Lead>(dto);

        await _dbContext.Leads.AddAsync(newLead);
        await _dbContext.SaveChangesAsync();

        var lead = await _dbContext.Leads
            .AsNoTracking()
            .FirstOrDefaultAsync(lead => lead.ContactId == newLead.ContactId);

        return new BaseResult<LeadResponse>() { Data = _mapper.Map<LeadResponse>(lead) };
    }

    public async Task<BaseResult<LeadResponse>> ChangeLeadStatus(long leadId, LeadStatus newStatus)
    {
        var lead = await _dbContext.Leads
            .AsNoTracking()
            .FirstOrDefaultAsync(lead => lead.ContactId == leadId);

        if (lead != null)
        {
            lead.LeadStatus = newStatus;
            await _dbContext.SaveChangesAsync();
            return new BaseResult<LeadResponse>() { Data = _mapper.Map<LeadResponse>(lead) };
        }

        return new BaseResult<LeadResponse>() { ErrorMessage = "This lead doesn't exist." };
    }
}
