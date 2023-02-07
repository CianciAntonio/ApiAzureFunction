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

        public async Task RemoveProduct(int id)
        {
            var dbProduct = await _appDbContext.Product.FindAsync(id);
                                    
            _appDbContext.Remove(dbProduct);
        }
    }
}
