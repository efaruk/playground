using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Energetic.Data.Entities;

namespace Energetic.DataAccess.Repository
{
    public class CustomerRepository : BaseRepository, ICustomerRepository
    {
        public CustomerRepository(string connectionString) : base(connectionString)
        {
            
        }

        public Customer GetCustomer(string id)
        {
            var result = Connection.Query<Customer>(@"select [PkCustomerId] as [Id], [Name] as [FirstName], [Surname] as [LastName] from Customer where PkCustomerId = @Id", new { Id = id }).FirstOrDefault();
            //var result = Connection.Query<Customer>(@"select [PkCustomerId] as [Id], [Name] as [FirstName], [Surname] as [LastName] from Customer").FirstOrDefault();
            return result;
        }

        public void SaveCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public void CreateTables()
        {
            Connection.Execute(@"CREATE TABLE [Customer] (
                                [PkCustomerId] VARCHAR (36)  PRIMARY KEY NOT NULL,
                                [Name] VARCHAR (50)  NOT NULL,
                                [Surname] VARCHAR (50)  NOT NULL
                            )");
        }
    }
}
