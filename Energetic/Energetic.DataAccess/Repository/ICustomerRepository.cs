using System.Collections.Generic;
using Energetic.Data.Entities;

namespace Energetic.DataAccess.Repository
{
    public interface ICustomerRepository
    {
        List<Customer> GetAll();

        Customer Get(string id);

        void Save(Customer customer);

        void CreateTable();
    }
}
