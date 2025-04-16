namespace SalesTechnicalAssignment.Models
{
    public class Sale
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string Customer { get; set; } = string.Empty;
        public string Branch { get; set; } = string.Empty;
        public List<SaleItem> Items { get; set; } = new();
        public bool IsCancelled { get; set; } = false;
        public decimal TotalAmount => Items.Sum(i => i.Total);
    }
}
