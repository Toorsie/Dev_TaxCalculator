using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Central.BusinessLogic
{
    public class TaxLogic
    {
        private string connectionString;
        public TaxLogic(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Models.TaxCalculatorDTO CalculateTax(Models.TaxInputDTO taxInputDTO)
        {
            PostalCodeLogic postalCodeLogic = new PostalCodeLogic(connectionString);
            PostalTaxRateLogic postalTaxRateLogic = new PostalTaxRateLogic(connectionString);
            var postalCode = postalCodeLogic.GetPostalCodeByGuid(taxInputDTO.PostalCodeGuid.Value);
            postalCode.PostalCodeTaxRates = postalTaxRateLogic.GetAllPostalTaxRatesByPostalCodeGuid(postalCode.RowGuid);
            Models.TaxCalculatorDTO taxCalculatorDTO = new Models.TaxCalculatorDTO(postalCode, taxInputDTO.AnnualAmount.Value);
            taxCalculatorDTO.Year = taxInputDTO.Year.Value;
            return taxCalculatorDTO;
        }

        public Guid InsertSubmittedTax(Models.TaxInputDTO taxInputDTO)
        {
            Data.Repository.PostalCodeRepo postalCodeRepo = new Data.Repository.PostalCodeRepo(connectionString);
            Data.Repository.SubmittedTaxRepo submittedTaxRepo = new Data.Repository.SubmittedTaxRepo(connectionString);
            var taxModel = new Data.Models.SubmittedTaxModel()
            {
                Amount = taxInputDTO.AnnualAmount.Value,
                PostalCodeId = postalCodeRepo.GetPostalCodeByGuid(taxInputDTO.PostalCodeGuid.Value).RowId,
                 Year = taxInputDTO.Year.Value
            };
            return submittedTaxRepo.GetSubmittedTaxById(submittedTaxRepo.Insert(taxModel)).RowGuid;
        }

        public Guid UpdateSubmittedTax(Models.TaxInputDTO taxInputDTO)
        {
            Data.Repository.PostalCodeRepo postalCodeRepo = new Data.Repository.PostalCodeRepo(connectionString);
            Data.Repository.SubmittedTaxRepo submittedTaxRepo = new Data.Repository.SubmittedTaxRepo(connectionString);
            var taxModel = new Data.Models.SubmittedTaxModel()
            {
                Amount = taxInputDTO.AnnualAmount.Value,
                PostalCodeId = postalCodeRepo.GetPostalCodeByGuid(taxInputDTO.PostalCodeGuid.Value).RowId,
                RowGuid = taxInputDTO.RowGuid,
                Year = taxInputDTO.Year.Value
            };
            submittedTaxRepo.UpdateSubmittedTax(taxModel);
            return taxInputDTO.RowGuid;
        }


        public List<Models.TaxCalculatorDTO> GetAllSubmittedTax()
        {
            PostalCodeLogic postalCodeLogic = new PostalCodeLogic(connectionString);
            PostalTaxRateLogic postalTaxRateLogic = new PostalTaxRateLogic(connectionString);

            Data.Repository.SubmittedTaxRepo submittedTaxRepo = new Data.Repository.SubmittedTaxRepo(connectionString);

            var submittedTaxList = submittedTaxRepo.GetSubmittedTaxList();
            List<Models.TaxCalculatorDTO> taxCalculators = new List<Models.TaxCalculatorDTO>();
            foreach (var item in submittedTaxList)
            {
                var postalCode = postalCodeLogic.GetPostalCodeById(item.PostalCodeId);
                postalCode.PostalCodeTaxRates = postalTaxRateLogic.GetAllPostalTaxRatesByPostalCodeGuid(postalCode.RowGuid);
                var taxCalculator = new Models.TaxCalculatorDTO(postalCode, item.Amount);
                taxCalculator.TaxInputGuid = item.RowGuid;
                taxCalculator.Year = item.Year;
                taxCalculators.Add(taxCalculator);
            }
            return taxCalculators;
        }

        public Models.TaxCalculatorDTO GetSubmittedTaxByGuid(Guid rowGuid)
        {
            PostalCodeLogic postalCodeLogic = new PostalCodeLogic(connectionString);
            PostalTaxRateLogic postalTaxRateLogic = new PostalTaxRateLogic(connectionString);


            Data.Repository.SubmittedTaxRepo submittedTaxRepo = new Data.Repository.SubmittedTaxRepo(connectionString);

            var submittedTax = submittedTaxRepo.GetSubmittedTaxByGuid(rowGuid);
            var postalCode = postalCodeLogic.GetPostalCodeById(submittedTax.PostalCodeId);
            postalCode.PostalCodeTaxRates = postalTaxRateLogic.GetAllPostalTaxRatesByPostalCodeGuid(postalCode.RowGuid);
            var taxCalculator = new Models.TaxCalculatorDTO(postalCode, submittedTax.Amount);
            taxCalculator.TaxInputGuid = submittedTax.RowGuid;
            taxCalculator.Year = submittedTax.Year;
            return taxCalculator;
        }

        public void RemoveSubmittedTax(Guid rowGuid)
        {
            Data.Repository.SubmittedTaxRepo submittedTaxRepo = new Data.Repository.SubmittedTaxRepo(connectionString);
            submittedTaxRepo.DeleteSubmittedTaxByGuid(rowGuid);
        }
    }
}
