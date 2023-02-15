using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos.ModelRequest
{
    public class InvoiceRequest
    {
        public int CustomerId { get; set; }
        public string OrderDate { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
    }
}
