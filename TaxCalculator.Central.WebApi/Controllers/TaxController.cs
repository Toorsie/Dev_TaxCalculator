using Microsoft.AspNetCore.Mvc;

namespace TaxCalculator.Central.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TaxController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public TaxController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost(Name = "CalculateTax")]
        public Models.TaxCalculatorDTO CalculateTax(Models.TaxInputDTO taxInputDTO)
        {
            Central.BusinessLogic.TaxLogic taxLogic = new BusinessLogic.TaxLogic(_configuration.GetValue<string>("ConnectionString"));
            return taxLogic.CalculateTax(taxInputDTO);
        }

        [HttpGet(Name = "GetPostalCodeList")]
        public List<Models.PostalCodeDTO> GetPostalCodeList()
        {
            Central.BusinessLogic.PostalCodeLogic postalCodeLogic = new BusinessLogic.PostalCodeLogic(_configuration.GetValue<string>("ConnectionString"));
            return postalCodeLogic.GetAllPostalCodes();
        }
        
        [HttpPost(Name = "SubmitTax")]
        public Models.TaxCalculatorDTO SubmitTax(Models.TaxInputDTO taxInputDTO)
        {
            Central.BusinessLogic.TaxLogic taxLogic = new BusinessLogic.TaxLogic(_configuration.GetValue<string>("ConnectionString"));
            var relGuid = taxLogic.InsertSubmittedTax(taxInputDTO);
            var returnVal = taxLogic.CalculateTax(taxInputDTO);
            returnVal.TaxInputGuid = relGuid;
            return returnVal;
        }

        [HttpPost(Name = "UpdateSubmittedTax")]
        public Models.TaxCalculatorDTO UpdateSubmittedTax(Models.TaxInputDTO taxInputDTO)
        {
            Central.BusinessLogic.TaxLogic taxLogic = new BusinessLogic.TaxLogic(_configuration.GetValue<string>("ConnectionString"));
            var returnGuid = taxLogic.UpdateSubmittedTax(taxInputDTO);
            var returnVal = taxLogic.CalculateTax(taxInputDTO);
            returnVal.TaxInputGuid = returnGuid;
            return returnVal;
        }

        [HttpGet(Name = "DeleteSubmittedTax")]
        public IActionResult DeleteSubmittedTax(Guid rowGuid)
        {
            Central.BusinessLogic.TaxLogic taxLogic = new BusinessLogic.TaxLogic(_configuration.GetValue<string>("ConnectionString"));
            taxLogic.RemoveSubmittedTax(rowGuid);
            return Ok();
        }

        [HttpGet(Name = "GetSubmittedTax")]
        public Models.TaxCalculatorDTO GetSubmittedTax(Guid rowGuid)
        {
            Central.BusinessLogic.TaxLogic taxLogic = new BusinessLogic.TaxLogic(_configuration.GetValue<string>("ConnectionString"));
            return taxLogic.GetSubmittedTaxByGuid(rowGuid);
        }
        [HttpGet(Name = "GetSubmittedTaxList")]
        public List<Models.TaxCalculatorDTO> GetSubmittedTaxList()
        {
            Central.BusinessLogic.TaxLogic taxLogic = new BusinessLogic.TaxLogic(_configuration.GetValue<string>("ConnectionString"));
            return taxLogic.GetAllSubmittedTax();
        }



    }
}