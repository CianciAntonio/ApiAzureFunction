using EntityFrameworkClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

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
                .Include(y => y.Invoices)
                .ThenInclude(x => x.Product)
                .ToListAsync();

            return customers;
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            Customer? customer = await _appDbContext.Customers
                .Include(y => y.Invoices)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);

            return customer;
        }

        public async Task AddCustomer(Customer customer)
        {
            await _appDbContext.Customers.AddAsync(customer);
        }

        public async Task<string> UpdateCustomer(Customer customer)
        {
            Customer? dbCustomer = await _appDbContext.Customers.FindAsync(customer.Id);

            if (dbCustomer == null)
                return "Id Not Found";

            dbCustomer.Name = customer.Name;
            dbCustomer.LastName = customer.LastName;
            _appDbContext.Customers.Update(dbCustomer);
            return "Customer Updated!";
        }

        public async Task<string> RemoveCostomerById(int id)
        {
            var dbCustomer = await _appDbContext.Customers.FindAsync(id);

            if (dbCustomer == null)
                return "Id Not Found";

            _appDbContext.Remove(dbCustomer);
            return "Customer Removed!";
        }
    }
}
