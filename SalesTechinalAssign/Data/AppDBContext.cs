using Microsoft.EntityFrameworkCore;
using SalesTechnicalAssignment.Models;
using System.Collections.Generic;

namespace SalesTechnicalAssignment.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Sale> Sales => Set<Sale>();
    }
}
