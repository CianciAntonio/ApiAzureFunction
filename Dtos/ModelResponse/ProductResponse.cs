using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos.CustomerResponse
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Product { get; set; }
        public int IdInvoice { get; set; }
    }
}
