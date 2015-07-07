using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElasticLinq;
using Nest;

namespace Goldfinch.Demo.Data
{
    [ElasticType(IdProperty = "PkId")]
    public class ComplexObject
    {
        [NotAnalyzed]
        [ElasticProperty(Type = FieldType.Integer, Index = FieldIndexOption.NotAnalyzed, Store = true)]
        [Key]
        public int PkId { get; set; }

        [NotAnalyzed]
        [ElasticProperty(Type = FieldType.String, Index = FieldIndexOption.NotAnalyzed, Store = true)]
        public string Name { get; set; }

        [NotAnalyzed]
        [ElasticProperty(Type = FieldType.Date, Index = FieldIndexOption.NotAnalyzed, Store = true)]
        public DateTime ModifiedDate { get; set; }

        [NotAnalyzed]
        [ElasticProperty(Type = FieldType.Boolean, Index = FieldIndexOption.NotAnalyzed, Store = true)]
        public bool IsCrew { get; set; }

        [NotAnalyzed]
        [ElasticProperty(Type = FieldType.Float, Index = FieldIndexOption.NotAnalyzed, Store = true)]
        public decimal Amount { get; set; }

        [ElasticProperty(Type = FieldType.Binary, Index = FieldIndexOption.No, Store = true)]
        public byte[] Data { get; set; }
    }
}
