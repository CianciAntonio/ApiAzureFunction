using EntityFrameworkClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Dtos;

namespace EntityFrameworkClassLibrary.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    { 
        private AppDbContext _appDbContext;

        public CustomerRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            var customers = await _appDbContext.Customers
                .AsNoTracking()
                .Include(y => y.Invoices)
                .ThenInclude(x => x.Product)
                .ToListAsync();

            return customers;
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            Customer customer = await _appDbContext.Customers
                .AsNoTracking()
                .Include(y => y.Invoices)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);

            return customer;
        }

        public async Task AddCustomer(Customer customer)
        {
            await _appDbContext.Customers.AddAsync(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            _appDbContext.Customers.Update(customer);
        }

        public void RemoveCustomer(Customer customer)
        {
            _appDbContext.Remove(customer);
        }
    }
}
