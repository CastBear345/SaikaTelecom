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

    /// <summary>
    /// Get all sales.
    /// </summary>
    /// <returns>A base result containing a list of sale responses or an error message.</returns>
    public async Task<BaseResult<List<SaleResponse>>> GetAllSales()
    {
        var sales = await _dbContext.Sales
            .AsNoTracking()
            .ToListAsync();

        if (sales != null)
            return new BaseResult<List<SaleResponse>>() { ErrorMessage = "There are no concluded contracts" };

        return new BaseResult<List<SaleResponse>>() { Data = _mapper.Map<List<SaleResponse>>(sales) };
    }

    /// <summary>
    /// Get sales by seller ID.
    /// </summary>
    /// <param name="sellerId">The ID of the seller.</param>
    /// <returns>A base result containing a list of sale responses or an error message.</returns>
    public async Task<BaseResult<List<SaleResponse>>> GetBySellerId(long sellerId)
    {
        var sales = await _dbContext.Sales
            .AsNoTracking()
            .Where(s => s.SellerId == sellerId)
        .ToListAsync();

        if (sales != null)
            return new BaseResult<List<SaleResponse>>() { ErrorMessage = "The seller has no contracts" };

        return new BaseResult<List<SaleResponse>>() { Data = _mapper.Map<List<SaleResponse>>(sales) };
    }

    /// <summary>
    /// Create a new sale with the provided information.
    /// </summary>
    /// <param name="dto">DTO containing sale details.</param>
    /// <returns>A base result containing the sale response or an error message.</returns>
    public async Task<BaseResult<SaleResponse>> CreateSale(SaleGetDto dto)
    {
        if (dto == null)
            return new BaseResult<SaleResponse>() { ErrorMessage = "Sale data is null." };

        var existingSale = await _dbContext.Sales
            .AsNoTracking()
            .FirstOrDefaultAsync(sale => sale.LeadId == dto.LeadId);

        if (existingSale != null) 
            return new BaseResult<SaleResponse>() { ErrorMessage = "An agreement has already been concluded with this lead." };

        var sale = _mapper.Map<Sale>(dto);

        await _dbContext.Sales.AddAsync(sale);
        await _dbContext.SaveChangesAsync();

        return new BaseResult<SaleResponse>() { Data = _mapper.Map<SaleResponse>(sale) };
    }
}
