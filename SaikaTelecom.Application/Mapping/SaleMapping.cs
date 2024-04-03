using AutoMapper;
using SaikaTelecom.Domain.Contracts.SaleDtos;
using SaikaTelecom.Domain.Entities;

namespace SaikaTelecom.Application.Mapping;

public class SaleMapping : Profile
{
    public SaleMapping()
    {
        CreateMap<Sale, SaleGetDto>()
            .ReverseMap();
    }
}
