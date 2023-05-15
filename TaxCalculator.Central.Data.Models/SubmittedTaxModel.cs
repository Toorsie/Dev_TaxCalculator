using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Central.Data.Models
{
    public class SubmittedTaxModel
    {
        public long RowId { get; set; }
        public Guid RowGuid { get; set; }
        public decimal Amount { get; set; }
        public long PostalCodeId { get; set; }
        public int Year { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
