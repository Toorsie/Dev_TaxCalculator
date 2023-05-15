using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Central.Models
{
    public class Enums
    {
        public enum TaxCalculationType
        {
            Progressive = 1,
            FlatValue = 2,
            FlatRate = 3

        }

        public enum RateType
        {
            Number = 1,
            Percentage = 2
        }
    }
}
