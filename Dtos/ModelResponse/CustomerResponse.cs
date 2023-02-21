namespace Dtos.CustomerResponse
{
    public class CustomerResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public virtual List<InvoiceResponse> Invoices { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}