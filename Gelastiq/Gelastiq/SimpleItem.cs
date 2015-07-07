using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace Gelastiq
{
    [ElasticType(IdProperty = "PkId")]
    public class SimpleItem
    {
        [ElasticProperty(Type = FieldType.Integer, Index = FieldIndexOption.NotAnalyzed, Store = true)]
        [Key]
        public int PkId { get; set; }

        [ElasticProperty(Type = FieldType.String, Index = FieldIndexOption.NotAnalyzed, Store = true)]
        public string ContainerName { get; set; }

        [ElasticProperty(Type = FieldType.String, Index = FieldIndexOption.NotAnalyzed, Store = true)]
        public string FieldName { get; set; }

        [ElasticProperty(Type = FieldType.String, Index = FieldIndexOption.No, Store = true)]
        public string FieldValue { get; set; }
    }
}
