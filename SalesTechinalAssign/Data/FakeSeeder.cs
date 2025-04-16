using SalesTechnicalAssignment.Models;
using SalesTechnicalAssignment.Data;

public static class FakeSeeder
{
    public static void Seed(AppDbContext context)
    {
        if (context.Sales.Any()) return;

        var sales = new List<Sale>
        {
            new Sale
            {
                Customer = "Vivian",
                Branch = "RJ",
                Items = new List<SaleItem>
                {
                    new() { ProductId = 1, ProductName = "Vestido", Quantity = 4, UnitPrice = 50, Discount = 20 }
                }
            },
            new Sale
            {
                Customer = "Guilherme",
                Branch = "SP",
                Items = new List<SaleItem>
                {
                    new() { ProductId = 2, ProductName = "Camisa", Quantity = 10, UnitPrice = 200, Discount = 400 }
                }
            }
        };

        context.Sales.AddRange(sales);
        context.SaveChanges();
    }
}
