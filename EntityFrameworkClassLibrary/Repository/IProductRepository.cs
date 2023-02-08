using EntityFrameworkClassLibrary.Models;

namespace EntityFrameworkClassLibrary.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task AddProduct(Product product);
        Task<Product> GetProduct(int id);
        void RemoveProduct(Product product);
    }
}