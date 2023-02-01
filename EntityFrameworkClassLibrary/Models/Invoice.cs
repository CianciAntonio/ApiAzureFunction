using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkClassLibrary.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public string OrderDate { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int CustomerId { get; set; }

        public Product Product { get; set; }
        public Customer Customer { get; set; }
    }
}
