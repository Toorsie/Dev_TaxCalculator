namespace TaxCalculator.Central.WebApplication.Models.APIModels
{
    public class GetTaxCalculationDTO : BasePostWebApiModel<Central.Models.TaxInputDTO, Central.Models.TaxCalculatorDTO>
    {
        
    }


    public class GetPostalCodeList : BaseGetWebApiModel<List<Central.Models.PostalCodeDTO>>
    {
        
    }

    public class GetTaxCalculationList : BaseGetWebApiModel<List<Central.Models.TaxCalculatorDTO>>
    {

    }

    public class GetSubmittedTax : BaseGetWebApiModel<Central.Models.TaxCalculatorDTO>
    {
        public Guid RowGuid { get; set; }
    }

    public class DeleteSubmittedTax : BaseGetWebApiModel<object>
    {
        public Guid RowGuid { get; set; }
    }
}
