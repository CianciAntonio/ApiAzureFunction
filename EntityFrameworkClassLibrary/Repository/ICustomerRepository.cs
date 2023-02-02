using EntityFrameworkClassLibrary.Models;

namespace EntityFrameworkClassLibrary.Repository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<IEnumerable<Customer>> GetAllCustomers();
        Task<Customer?> GetCustomerById(int id);
        Task AddCustomer(Customer customer);
        Task<string> UpdateCustomer(Customer customer);
        Task<string> RemoveCostomerById(int id);
    }
}
