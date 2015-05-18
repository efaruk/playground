using System.Data.Linq.Mapping;

namespace Energetic.Data.Entities
{
    public class Customer
    {
        [Column(Name = "PkCustomerId")]
        public string Id { get; set; }

        [Column(Name = "Name")]
        public string FirstName { get; set; }

        [Column(Name = "Surname")]
        public string LastName { get; set; }
    }
}
