using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Central.Data.Repository;

namespace TaxCalculator.Central.BusinessLogic
{
    public class PostalTaxRateLogic
    {
        private string connectionString;
        public PostalTaxRateLogic(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public List<Models.PostalCodeTaxRateDTO> GetAllPostalTaxRates()
        {
            List<Models.PostalCodeTaxRateDTO> postalTaxRateDTOs = new List<Models.PostalCodeTaxRateDTO>();
            PostalTaxRateRepo postalTaxRateRepo = new PostalTaxRateRepo(connectionString);
            foreach (var postalTaxRate in postalTaxRateRepo.ListAllPostalTaxRateModels())
            {
                postalTaxRateDTOs.Add(MapPostalTaxRateDTO(postalTaxRate));
            }
            return postalTaxRateDTOs;

        }

        public List<Models.PostalCodeTaxRateDTO> GetAllPostalTaxRatesByPostalCodeGuid(Guid postalCodeGuid)
        {
            List<Models.PostalCodeTaxRateDTO> postalTaxRateDTOs = new List<Models.PostalCodeTaxRateDTO>();
            PostalTaxRateRepo postalTaxRateRepo = new PostalTaxRateRepo(connectionString);
            foreach (var postalTaxRate in postalTaxRateRepo.ListAllPostalTaxRateModelsByPostalCodeId(new PostalCodeRepo(connectionString).GetPostalCodeByGuid(postalCodeGuid).RowId))
            {
                postalTaxRateDTOs.Add(MapPostalTaxRateDTO(postalTaxRate));
            }
            return postalTaxRateDTOs;

        }

        public Models.PostalCodeTaxRateDTO GetPostalTaxRateByGuid(Guid rowGuid)
        {
            PostalTaxRateRepo postalTaxRateRepo = new PostalTaxRateRepo(connectionString);
            return MapPostalTaxRateDTO(postalTaxRateRepo.GetPostalTaxRateByGuid(rowGuid));
        }

        public Models.PostalCodeTaxRateDTO MapPostalTaxRateDTO(Data.Models.PostalTaxRateModel postalTaxRateModel)
        {
            return new Models.PostalCodeTaxRateDTO() {
                Created = postalTaxRateModel.Created,
                FromValue = postalTaxRateModel.FromValue,
                PostalCodeGuid = new PostalCodeRepo(connectionString).GetPostalCodeById(postalTaxRateModel.PostalCodeId).RowGuid,
                Rate = postalTaxRateModel.Rate,
                RateType = (Models.Enums.RateType)postalTaxRateModel.RateTypeId,
                ToValue = postalTaxRateModel.ToValue,
                Updated = postalTaxRateModel.Updated
            };
        }
    }
}
