namespace Dtos.CustomerResponse
{
    public class InvoiceResponse
    {
        public int Id { get; set; }
        public ProductResponse ProductDescription { get; set; }
        public int IdCustomer { get; set; }
        public string Order { get; set; }
        public string Customer { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
