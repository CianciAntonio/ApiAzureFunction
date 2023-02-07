using EntityFrameworkClassLibrary.Models;

namespace EntityFrameworkClassLibrary.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task AddProduct(Product product);
        Task RemoveProduct(int id);
    }
}