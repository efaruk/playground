using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Energetic.Data.Entities;

namespace Energetic.DataAccess.Repository
{
    public class CustomerRepository : BaseRepository, ICustomerRepository
    {
        public CustomerRepository(string connectionString)
            : base(connectionString)
        {
            SqlMapper.SetTypeMap(typeof(Customer), new CustomPropertyTypeMap(typeof(Customer),
                (type, columnName) => type.GetProperties().FirstOrDefault(prop => prop.GetCustomAttributes(false)
                .OfType<ColumnAttribute>()
                .Any(attr => attr.Name == columnName))));
        }

        public List<Customer> GetAll()
        {
            var result = Connection.Query<Customer>(@"select PkCustomerId, Name, Surname from Customer").ToList();
            //var result = Connection.Query<Customer>(@"select [PkCustomerId] as [Id], [Name] as [FirstName], [Surname] as [LastName] from Customer").FirstOrDefault();
            return result;
        }

        public Customer Get(string id)
        {
            var result = Connection.Query<Customer>(@"select PkCustomerId, Name, Surname from Customer where PkCustomerId = @Id", new { Id = id }).FirstOrDefault();
            //var result = Connection.Query<Customer>(@"select [PkCustomerId] as [Id], [Name] as [FirstName], [Surname] as [LastName] from Customer").FirstOrDefault();
            return result;
        }

        public void Save(Customer customer)
        {
            if (string.IsNullOrWhiteSpace(customer.Id) || customer.Id.Length != 36)
                customer.Id = Guid.NewGuid().ToString();
            Connection.Execute(@"insert into Customer Values (@Id, @FirstName, @LastName)", customer);
        }

        public void CreateTable()
        {
            Connection.Execute(@"CREATE TABLE [Customer] (
                                [PkCustomerId] VARCHAR (36)  PRIMARY KEY NOT NULL,
                                [Name] VARCHAR (50)  NOT NULL,
                                [Surname] VARCHAR (50)  NOT NULL
                            )");
        }
    }
}
