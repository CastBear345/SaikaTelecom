using Microsoft.EntityFrameworkCore;
using SaikaTelecom.DAL;
using SaikaTelecom.Domain.Entities;
using SaikaTelecom.Domain.Enum;

namespace SaikaTelecom.Application.Services;

public class LeadService
{
    private readonly ApplicationDbContext _dbContext;

    public LeadService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Lead>> GetSalespersonLeads(long salespersonId)
    {
        return await _dbContext.Leads
            .Where(l => l.SellerId == salespersonId)
            .ToListAsync();
    }

    public async Task CreateLead(long contactId, long salespersonId)
    {
        var lead = new Lead()
        {
            ContactId = contactId,
            SellerId = salespersonId,
            LeadStatus = LeadStatus.New
        };
        await _dbContext.Leads.AddAsync(lead);
        await _dbContext.SaveChangesAsync();
    }

    public async Task ChangeLeadStatus(long leadId, LeadStatus newStatus)
    {
        var lead = await _dbContext.Leads.FindAsync(leadId);
        if (lead != null)
        {
            lead.LeadStatus = newStatus;
            await _dbContext.SaveChangesAsync();
        }
    }
}
