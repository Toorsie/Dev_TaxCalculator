using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Central.Data.Models;

namespace TaxCalculator.Central.Data.Repository
{
    public class PostalTaxRateRepo
    {
        private string connectionString;
        public PostalTaxRateRepo(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<PostalTaxRateModel> ListAllPostalTaxRateModels()
        {
            List<PostalTaxRateModel> postalTaxRateModels = new List<PostalTaxRateModel>();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string query = $"SELECT * FROM lookup.PostalCodeTaxRate";
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                using (var reader = sqlCommand.ExecuteReader())
                {
                    postalTaxRateModels = reader.MapToList<PostalTaxRateModel>();
                }
            }
            return postalTaxRateModels;
        }

        public List<PostalTaxRateModel> ListAllPostalTaxRateModelsByPostalCodeId(long postalCodeId)
        {
            List<PostalTaxRateModel> postalTaxRateModels = new List<PostalTaxRateModel>();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string query = $"SELECT * FROM lookup.PostalCodeTaxRate WHERE PostalCodeId = {postalCodeId}";
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                using (var reader = sqlCommand.ExecuteReader())
                {
                    postalTaxRateModels = reader.MapToList<PostalTaxRateModel>();
                }
            }
            return postalTaxRateModels;
        }

        public PostalTaxRateModel GetPostalTaxRateByGuid(Guid rowGuid)
        {
            PostalTaxRateModel postalTaxRateModel = new PostalTaxRateModel();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string query = $"SELECT * FROM lookup.PostalCodeTaxRate WHERE RowGuid = '{rowGuid}'";
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                using (var reader = sqlCommand.ExecuteReader())
                {
                    postalTaxRateModel = reader.MapToSingle<PostalTaxRateModel>();
                }
            }
            return postalTaxRateModel;
        }
    }
}
