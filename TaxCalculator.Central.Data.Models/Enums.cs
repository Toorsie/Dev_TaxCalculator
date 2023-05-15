using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TaxCalculator.Central.Data.Models
{
    public class Enums
    {
        public enum TaxCalculationType
        {
            [Display(Name = "Progressive")]
            Progressive = 1,
            [Display(Name = "Flat Value")]
            FlatValue = 2,
            [Display(Name = "Flat Rate")]
            FlatRate = 3
        }

        public enum RateType
        {
            Number = 1,
            Percentage = 2
        }
    }
}
