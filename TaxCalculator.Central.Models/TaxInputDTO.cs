using System.ComponentModel.DataAnnotations;
using static TaxCalculator.Central.Models.Enums;

namespace TaxCalculator.Central.Models
{
    public class TaxInputDTO
    {
        public Guid RowGuid { get; set; }  
        [Display(Name = "Postal Code")]
        [Required]
        public Guid? PostalCodeGuid { get; set; }
        [Display(Name = "Year")]
        [Required]
        public int? Year { get; set; }
        [Display(Name = "Annual amount earned")]
        [Required]
        public decimal? AnnualAmount { get; set; }
    }


    public class TaxCalculatorDTO
    {
        public Guid TaxInputGuid { get; set; }
        public int Year { get; set; }
        public decimal AnnualAmount { get; }
        public decimal TaxPayableAmount { get; }
        public decimal NetAmount { get; }
        public PostalCodeDTO PostalCode { get; set; }
        public TaxCalculatorDTO(PostalCodeDTO PostalCode, decimal AnnualAmount)
        {
            this.PostalCode = PostalCode;
            this.AnnualAmount = AnnualAmount;
            TaxPayableAmount = CalculateYearlyTax();
            NetAmount = CalculateAmountAfterTax();
        }

        
        protected decimal CalculateAmountAfterTax()
        {
            return AnnualAmount - TaxPayableAmount;
        }

        private decimal CalculateYearlyTax()
        {
            decimal taxAmount = 0;

            var rates = PostalCode.PostalCodeTaxRates.Where(e => e.FromValue <= AnnualAmount).OrderByDescending(e => e.FromValue);
            if (rates == null)
            {
                throw new Exception($"No rate could be found for Postal Code {PostalCode.PostalCode} : {PostalCode.RowGuid}");
            }
            switch (PostalCode.TaxCalculationType)
            {
                case TaxCalculationType.Progressive:
                    var unTaxedAmount = AnnualAmount;
                    foreach (var rate in rates)
                    {
                        var AmountPaid = (unTaxedAmount - rate.FromValue) * (rate.Rate / 100);
                        taxAmount += AmountPaid;
                        unTaxedAmount = rate.FromValue-1;
                    }
                    break;

                case TaxCalculationType.FlatValue:
                    var relRate = rates.Where(e => e.FromValue <= AnnualAmount && (e.ToValue >= AnnualAmount || e.ToValue == null));
                    if (relRate.Single().RateType == RateType.Percentage)
                    {
                        taxAmount += AnnualAmount * (relRate.Single().Rate / 100);
                    }
                    else
                    {
                        taxAmount += relRate.Single().Rate;
                    }
                    break;


                case TaxCalculationType.FlatRate:


                    if (rates.Single().RateType == RateType.Percentage)
                    {
                        taxAmount += AnnualAmount * (rates.Single().Rate / 100);
                    }
                    else
                    {
                        taxAmount += rates.Single().Rate;
                    }
                    break;
               

            }
            return taxAmount;
        }
    }

    public class PostalCodeDTO
    {
        public Guid RowGuid { get; set; }
        public string PostalCode { get; set; }
        public TaxCalculationType TaxCalculationType { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public List<PostalCodeTaxRateDTO> PostalCodeTaxRates { get; set; }

    }

    public class PostalCodeTaxRateDTO
    {
        private Guid RowGuid { get; set; }
        public Guid PostalCodeGuid { get; set; }
        public decimal Rate { get; set; }

        public decimal FromValue { get; set; }
        public decimal? ToValue { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public RateType RateType { get; set; }

    }

  
}