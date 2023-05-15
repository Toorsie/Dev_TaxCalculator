using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TaxCalculator.Central.Data.Models.Enums;

namespace TaxCalculator.Central.Data.Models
{
    
        public class PostalCodeModel
        {
            public Guid RowGuid { get; set; }
            public long RowId { get; set; }
            public string PostalCode { get; set; }
            public TaxCalculationType TaxCalculationTypeId { get; set; }
            public DateTime Created { get; set; }
            public DateTime Updated { get; set; }
            public List<PostalTaxRateModel> PostalCodeTaxRateList { get; set; }

        }

       

    
}
