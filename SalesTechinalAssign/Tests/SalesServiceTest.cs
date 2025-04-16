using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesTechnicalAssignment.Data;
using SalesTechnicalAssignment.DTO;
using SalesTechnicalAssignment.Interface;
using SalesTechnicalAssignment.Mapping;
using SalesTechnicalAssignment.Service;
using Xunit;

namespace SalesTechnicalAssignment.Tests
{
    public class SaleServiceTests
    {
        private ISalesService GetService()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<SaleProfile>());
            var mapper = config.CreateMapper();

            return new SaleService(context, mapper);
        }

        [Fact]
        public void Apply10PercentDiscount_WhenQuantityIs5()
        {
            var service = GetService();
            var request = new SaleRequest
            {
                Customer = "Carlos",
                Branch = "SP",
                Items = new() { new() { ProductId = 1, ProductName = "Item A", Quantity = 5, UnitPrice = 100 } }
            };

            var result = service.Create(request);
            Assert.Equal(450, result.TotalAmount);
        }

        [Fact]
        public void Apply20PercentDiscount_WhenQuantityIs15()
        {
            var service = GetService();
            var request = new SaleRequest
            {
                Customer = "João Silva",
                Branch = "RJ",
                Items = new() { new() { ProductId = 2, ProductName = "Item B", Quantity = 15, UnitPrice = 10 } }
            };

            var result = service.Create(request);
            Assert.Equal(120, result.TotalAmount);
        }

        [Fact]
        public void ThrowsException_WhenQuantityAbove20()
        {
            var service = GetService();
            var request = new SaleRequest
            {
                Customer = "Debora",
                Branch = "BA",
                Items = new() { new() { ProductId = 3, ProductName = "Item C", Quantity = 21, UnitPrice = 5 } }
            };

            Assert.Throws<ArgumentException>(() => service.Create(request));
        }
    }
}
