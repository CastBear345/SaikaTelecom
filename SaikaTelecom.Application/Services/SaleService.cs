using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SaikaTelecom.DAL;
using SaikaTelecom.Domain.Contracts.SaleDtos;
using SaikaTelecom.Domain.Entities;

namespace SaikaTelecom.Application.Services;

public class SaleService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public SaleService(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<Sale>> GetAllSales()
    {
        return await _dbContext.Sales.ToListAsync();
    }

    public async Task<List<Sale>> GetBySellerId(long sellerId)
    {
        return await _dbContext.Sales.Where(s => s.SellerId == sellerId).ToListAsync();
    }

    public async Task CreateSale(SaleGetDto dto)
    {
        var sale = _mapper.Map<Sale>(dto);

        await _dbContext.Sales.AddAsync(sale);
        await _dbContext.SaveChangesAsync();
    }
}
