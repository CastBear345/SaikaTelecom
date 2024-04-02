using SaikaTelecom.DAL;
using SaikaTelecom.Domain.Entities;
using SaikaTelecom.Domain.Enum;
using Microsoft.EntityFrameworkCore;

namespace SaikaTelecom.Application.Services
{
    public class ContactServices
    {
        private readonly ApplicationDbContext _dbContext;

        public ContactServices(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Contact>> GetAllContacts()
        {
            return await _dbContext.Contacts.ToListAsync();
        }

        public async Task<List<Contact>> GetLeadContacts()
        {
            return await _dbContext.Contacts.Where(c => c.Status == Status.Lead).ToListAsync();
        }

        public async Task CreateContact(Contact contact)
        {
            await _dbContext.Contacts.AddAsync(contact);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateContact(Contact contact)
        {
            _dbContext.Contacts.Update(contact);
            await _dbContext.SaveChangesAsync();
        }

        public async Task ChangeContactStatus(long contactId, Status newStatus)
        {
            var contact = await _dbContext.Contacts.FindAsync(contactId);
            if (contact != null)
            {
                contact.Status = newStatus;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
