using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TaxCalculator.Central.Data.Models.Enums;

namespace TaxCalculator.Central.Data.Models
{
    public class PostalTaxRateModel
    {
        public long RowId { get; set; }
        private Guid RowGuid { get; set; }
        public long PostalCodeId { get; set; }
        public decimal Rate { get; set; }
        public RateType RateTypeId { get; set; }
        public decimal FromValue { get; set; }
        public decimal? ToValue { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
      

        
    }
}
