using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Energetic.Data.Entities;

namespace Energetic.DataAccess.Mapping
{
    public sealed class MappingContainer
    {
        private static readonly MappingContainer _instance = new MappingContainer();
        public static MappingContainer Instance
        {
            get
            {
                return _instance;
            }
        }

        private MappingContainer()
        {
            //Initalize();
        }

        private void Initalize()
        {
            //var assembly = typeof (EnergeticDataEntityBase).Assembly;
            //var types = assembly.GetTypes();
            //foreach (var t in types)
            //{
            //    SqlMapper.SetTypeMap(typeof(Customer), new CustomPropertyTypeMap(t,
            //    (type, columnName) => type.GetProperties().FirstOrDefault(prop => prop.GetCustomAttributes(false)
            //    .OfType<ColumnAttribute>()
            //    .Any(attr => attr.Name == columnName))));
            //}
            
        }
    }
}
