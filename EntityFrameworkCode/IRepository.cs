using EntityFrameworkCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EntityFrameworkCode
{
    public interface IRepository
    {
        IEnumerable<Customer> Get();
        Customer Get(int id);
        void Add(Customer entity);
        void Update(int id);
        void Delete(int id);
    }
}
