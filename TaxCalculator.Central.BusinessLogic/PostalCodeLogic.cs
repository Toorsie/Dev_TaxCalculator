using TaxCalculator.Central.Data.Repository;

namespace TaxCalculator.Central.BusinessLogic
{
    public class PostalCodeLogic
    {
        private string connectionString;
        public PostalCodeLogic(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Models.PostalCodeDTO> GetAllPostalCodes()
        {
            List<Models.PostalCodeDTO> postalCodeDTOs = new List<Models.PostalCodeDTO>();
            PostalCodeRepo postalCodeRepo = new PostalCodeRepo(connectionString);
            foreach(var postalCode in postalCodeRepo.ListAllPostalCodes())
            {
                postalCodeDTOs.Add(MapPostalCodeDTO(postalCode));
            }
            return postalCodeDTOs;

        }

        public Models.PostalCodeDTO GetPostalCodeByGuid(Guid rowGuid)
        {
            PostalCodeRepo postalCodeRepo = new PostalCodeRepo(connectionString);
            return MapPostalCodeDTO(postalCodeRepo.GetPostalCodeByGuid(rowGuid));
        }

        public Models.PostalCodeDTO GetPostalCodeById(long rowId)
        {
            PostalCodeRepo postalCodeRepo = new PostalCodeRepo(connectionString);
            return MapPostalCodeDTO(postalCodeRepo.GetPostalCodeById(rowId));
        }

        public Models.PostalCodeDTO MapPostalCodeDTO(Data.Models.PostalCodeModel postalCodeModel)
        {
            Models.PostalCodeDTO postalCodeDTO = new Models.PostalCodeDTO();
            postalCodeDTO.PostalCode = postalCodeModel.PostalCode;
            postalCodeDTO.Updated = postalCodeModel.Updated;
            postalCodeDTO.Created = postalCodeModel.Created;
            postalCodeDTO.PostalCodeTaxRates = new List<Models.PostalCodeTaxRateDTO>();
            postalCodeDTO.TaxCalculationType = (Models.Enums.TaxCalculationType)postalCodeModel.TaxCalculationTypeId;
            postalCodeDTO.RowGuid = postalCodeModel.RowGuid;

            return postalCodeDTO;
        }


    }
}