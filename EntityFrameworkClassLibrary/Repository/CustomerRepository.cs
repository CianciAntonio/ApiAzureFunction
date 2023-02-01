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
            var customer = await _appDbContext.Customers.Include(y => y.Invoices).FirstOrDefaultAsync(x => x.Id == id);

            return customer;
        }

        public async Task AddCustomer(Customer customer)
        {
            await _appDbContext.Customers.AddAsync(customer);
        }

        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            Customer? dbCustomer = await _appDbContext.Customers.FindAsync(customer.Id);

            if (dbCustomer == null)
                return null;

            dbCustomer.Name = customer.Name;
            dbCustomer.LastName = customer.LastName;

            _appDbContext.Customers.Update(dbCustomer);

            return dbCustomer;
        }

        public async Task RemoveCostomerById(int id)
        {
            var dbCustomer = await _appDbContext.Customers.FindAsync(id);
            _appDbContext.Remove(dbCustomer);
        }
    }
}
