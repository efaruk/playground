using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FluentValidationAutoFacMvcDemo.Models
{
    public class PersonModel : BaseModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

    }

    public class BaseModel
    {
        public Guid Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}