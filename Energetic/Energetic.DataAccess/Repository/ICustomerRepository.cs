using Energetic.Data.Entities;

namespace Energetic.DataAccess.Repository
{
    public interface ICustomerRepository
    {
        Customer GetCustomer(string id);

        void SaveCustomer(Customer customer);

        void CreateTables();
    }
}
