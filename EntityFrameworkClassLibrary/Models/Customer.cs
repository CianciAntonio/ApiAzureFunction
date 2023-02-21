namespace EntityFrameworkClassLibrary.Models
{
    public class Customer : Timestamp
    {
        public int Id { get; set; }
        public string SurName { get; set; }
        public string LastName { get; set; }
        public virtual List<Invoice> Invoices { get; set; }
    }
}