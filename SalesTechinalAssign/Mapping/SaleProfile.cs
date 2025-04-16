using AutoMapper;
using SalesTechnicalAssignment.DTO;
using SalesTechnicalAssignment.Models;

namespace SalesTechnicalAssignment.Mapping
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<SaleRequest, Sale>()
                .ForMember(dest => dest.Items, opt => opt.Ignore());

            CreateMap<SaleItemRequest, SaleItem>()
                .ForMember(dest => dest.Discount, opt => opt.Ignore());
        }
    }
}
