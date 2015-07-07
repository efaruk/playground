using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElasticLinq;
using ElasticLinq.Mapping;

namespace Goldfinch
{
    public class NestElasticLinqMapping: ElasticMapping
    {
        public NestElasticLinqMapping(EnumFormat enumFormat = EnumFormat.String, CultureInfo conversionCulture = null): base(camelCaseFieldNames: true, camelCaseTypeNames: false, pluralizeTypeNames: false, lowerCaseAnalyzedFieldValues: true, enumFormat: enumFormat, conversionCulture: conversionCulture)
        {
            
        }

        public override string GetDocumentType(Type type)
        {
            var name = base.GetDocumentType(type).ToLowerInvariant();
            return name;
        }
    }
}
