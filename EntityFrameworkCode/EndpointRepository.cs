using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using EntityFrameworkCode.Models;

namespace EntityFrameworkCode
{
    public class EndpointRepository : IRepository
    {
        private readonly AppDbContext _dbContext;

        public EndpointRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Customer> Get()
        {
            return _dbContext.Customers.ToList();
        }

        public Customer Get(int id)
        {
            return _dbContext.Customers.FirstOrDefault(e => e.Id == id);
        }

        public void Add(Customer entity)
        {
            _dbContext.Customers.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(int id)
        {
            var entity = _dbContext.Customers.FirstOrDefault(e => e.Id == id);
            _dbContext.Customers.Update(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _dbContext.Customers.FirstOrDefault(e => e.Id == id);
            _dbContext.Customers.Remove(entity);
            _dbContext.SaveChanges();
        }
    }
}
