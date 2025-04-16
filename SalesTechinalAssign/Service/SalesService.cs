using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesTechnicalAssignment.Data;
using SalesTechnicalAssignment.DTO;
using SalesTechnicalAssignment.Interface;
using SalesTechnicalAssignment.Models;

namespace SalesTechnicalAssignment.Service
{
    public class SaleService : ISalesService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public SaleService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<Sale> GetAll(string? customer, string? branch, string? orderBy, int page = 1, int size = 10)
        {
            var query = _context.Sales.Include(s => s.Items).AsQueryable();

            if (!string.IsNullOrEmpty(customer))
                query = query.Where(s => s.Customer.Contains(customer));

            if (!string.IsNullOrEmpty(branch))
                query = query.Where(s => s.Branch.Contains(branch));

            if (!string.IsNullOrEmpty(orderBy))
            {
                query = orderBy.ToLower() switch
                {
                    "date desc" => query.OrderByDescending(s => s.Date),
                    "date" => query.OrderBy(s => s.Date),
                    "customer" => query.OrderBy(s => s.Customer),
                    "customer desc" => query.OrderByDescending(s => s.Customer),
                    "branch" => query.OrderBy(s => s.Branch),
                    "branch desc" => query.OrderByDescending(s => s.Branch),
                    _ => query.OrderBy(s => s.Date)
                };
            }

            return query.Skip((page - 1) * size).Take(size).ToList();
        }

        public Sale? GetById(Guid id) => _context.Sales
            .Include(s => s.Items)
            .FirstOrDefault(s => s.Id == id);

        public Sale Create(SaleRequest request)
        {
            var sale = _mapper.Map<Sale>(request);
            sale.Items = request.Items.Select(i =>
            {
                var item = _mapper.Map<SaleItem>(i);
                item.Discount = CalculateDiscount(i.Quantity, i.UnitPrice);
                return item;
            }).ToList();

            _context.Sales.Add(sale);
            _context.SaveChanges();
            Console.WriteLine($"SaleCreated: {sale.Id}");
            return sale;
        }

        public Sale? Update(Guid id, SaleRequest request)
        {
            var sale = _context.Sales.FirstOrDefault(s => s.Id == id);
            if (sale == null) return null;

            _mapper.Map(request, sale);
            sale.Items = request.Items.Select(i =>
            {
                var item = _mapper.Map<SaleItem>(i);
                item.Discount = CalculateDiscount(i.Quantity, i.UnitPrice);
                return item;
            }).ToList();

            _context.SaveChanges();
            Console.WriteLine($"SaleModified: {sale.Id}");
            return sale;
        }

        public bool Cancel(Guid id)
        {
            var sale = _context.Sales.FirstOrDefault(s => s.Id == id);
            if (sale == null || sale.IsCancelled) return false;

            sale.IsCancelled = true;
            _context.SaveChanges();
            Console.WriteLine($"SaleCancelled: {sale.Id}");
            return true;
        }

        private decimal CalculateDiscount(int quantity, decimal unitPrice)
        {
            if (quantity > 20)
                throw new ArgumentException("Cannot sell more than 20 items.");
            if (quantity >= 10)
                return unitPrice * quantity * 0.2m;
            if (quantity >= 4)
                return unitPrice * quantity * 0.1m;
            return 0m;
        }
    }
}
