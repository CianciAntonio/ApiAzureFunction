namespace Dtos.CustomerResponse
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Product { get; set; }
        public int IdInvoice { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
