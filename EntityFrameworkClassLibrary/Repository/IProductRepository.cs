using EntityFrameworkClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkClassLibrary.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task AddProduct(Product product);
        Task<string> RemoveProductById(int id);
    }
}
