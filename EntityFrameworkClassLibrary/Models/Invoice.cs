
namespace EntityFrameworkClassLibrary.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public string OrderDate { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public int CustomerId { get; set; }

        public Product Product { get; set; }
        public Customer Customer { get; set; }
    }
}
