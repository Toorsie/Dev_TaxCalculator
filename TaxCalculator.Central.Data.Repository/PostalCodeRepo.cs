using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Central.Data.Models;

namespace TaxCalculator.Central.Data.Repository
{
    public class PostalCodeRepo
    {
        private string connectionString;
        public PostalCodeRepo(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public PostalCodeModel GetPostalCodeById(long rowId)
        {
            PostalCodeModel postalCodeModel = new PostalCodeModel();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string query = $"SELECT * FROM lookup.PostalCode WHERE RowId = {rowId}";
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                using (var reader = sqlCommand.ExecuteReader())
                {
                    postalCodeModel = reader.MapToSingle<PostalCodeModel>();
                }
            }
            return postalCodeModel;
        }

        

        public PostalCodeModel GetPostalCodeByGuid(Guid rowGuid)
        {
            PostalCodeModel postalCodeModel = new PostalCodeModel();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string query = $"SELECT * FROM lookup.PostalCode WHERE RowGuid = '{rowGuid}'";
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                using (var reader = sqlCommand.ExecuteReader())
                {
                    postalCodeModel = reader.MapToSingle<PostalCodeModel>();
                }
            }
            return postalCodeModel;
        }


        public List<PostalCodeModel> ListAllPostalCodes()
        {
            List<PostalCodeModel> postalCodeModels = new List<PostalCodeModel>();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string query = $"SELECT * FROM lookup.PostalCode";
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                using (var reader = sqlCommand.ExecuteReader())
                {
                    postalCodeModels = reader.MapToList<PostalCodeModel>();
                }
            }
            return postalCodeModels;
        }


    }
}
