using EntityFrameworkClassLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkClassLibrary.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private AppDbContext _appDbContext;

        public ProductRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        public async Task<Product> GetProduct(int id)
        {
            Product product = await _appDbContext.Product
                .AsNoTracking() // Regalo da Loris: studiare perché ottimizza le query
                .Include(y => y.Invoice)
                .ThenInclude(y => y.Customer)
                .FirstOrDefaultAsync(x => x.Id == id);

            return product;
        }

        public async Task AddProduct(Product product)
        {
            await _appDbContext.Product.AddAsync(product);
        }

        public void RemoveProduct(Product product)
        {                            
            _appDbContext.Remove(product);
        }
    }
}
