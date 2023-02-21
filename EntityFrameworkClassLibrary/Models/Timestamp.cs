namespace EntityFrameworkClassLibrary.Models
{
    public abstract class Timestamp
    {
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
