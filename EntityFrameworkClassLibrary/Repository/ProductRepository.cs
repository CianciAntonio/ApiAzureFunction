using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkClassLibrary.Models;

namespace EntityFrameworkClassLibrary.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private AppDbContext _appDbContext;

        public ProductRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        public async Task AddProduct(Product product)
        {
            await _appDbContext.Product.AddAsync(product);
        }

        public async Task<string> RemoveProductById(int id)
        {
            var dbProduct = await _appDbContext.Product.FindAsync(id);

            if (dbProduct == null)
                return "Id Not Found";

            _appDbContext.Remove(dbProduct);
            return "Customer Removed!";
        }
    }
}
