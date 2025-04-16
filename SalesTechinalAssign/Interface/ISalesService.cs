using SalesTechnicalAssignment.DTO;
using SalesTechnicalAssignment.Models;

namespace SalesTechnicalAssignment.Interface
{
    public interface ISalesService
    {
        IEnumerable<Sale> GetAll(string? customer, string? branch, string? orderBy, int page = 1, int size = 10);
        Sale? GetById(Guid id);
        Sale Create(SaleRequest request);
        Sale? Update(Guid id, SaleRequest request);
        bool Cancel(Guid id);
    }
}
