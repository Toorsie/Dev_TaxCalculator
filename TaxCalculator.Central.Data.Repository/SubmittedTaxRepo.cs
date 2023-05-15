using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Central.Data.Repository
{
    public class SubmittedTaxRepo
    {
        private string connectionString;
        public SubmittedTaxRepo(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Data.Models.SubmittedTaxModel GetSubmittedTaxByGuid(Guid rowGuid)
        {

            Data.Models.SubmittedTaxModel submittedTaxModel = new Data.Models.SubmittedTaxModel();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string query = $"SELECT * FROM master.SubmittedTax WHERE RowGuid = '{rowGuid}'";
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                using (var reader = sqlCommand.ExecuteReader())
                {
                    submittedTaxModel = reader.MapToSingle<Data.Models.SubmittedTaxModel>();
                }
            }
            return submittedTaxModel;
            
        }

        public List<Data.Models.SubmittedTaxModel> GetSubmittedTaxList()
        {
            List<Data.Models.SubmittedTaxModel> submittedTaxModels = new List<Data.Models.SubmittedTaxModel>();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string query = $"SELECT * FROM master.SubmittedTax";
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                using (var reader = sqlCommand.ExecuteReader())
                {
                    submittedTaxModels = reader.MapToList<Data.Models.SubmittedTaxModel>();
                }
            }
            return submittedTaxModels;
        }

        public Data.Models.SubmittedTaxModel GetSubmittedTaxById(long rowId)
        {
            Data.Models.SubmittedTaxModel submittedTaxModel = new Data.Models.SubmittedTaxModel();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string query = $"SELECT * FROM master.SubmittedTax WHERE RowId = '{rowId}'";
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                using (var reader = sqlCommand.ExecuteReader())
                {
                    submittedTaxModel = reader.MapToSingle<Data.Models.SubmittedTaxModel>();
                }
            }
            return submittedTaxModel;
        }

        public long Insert(Data.Models.SubmittedTaxModel submittedTaxModel)
        {
            long rowId;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string query = $"INSERT INTO master.SubmittedTax (PostalCodeId, Amount, Year) OUTPUT INSERTED.RowId VALUES ({submittedTaxModel.PostalCodeId},{submittedTaxModel.Amount.ToString().Replace(",", ".")}, {submittedTaxModel.Year})";
                sqlConnection.Open();

                
                SqlParameter GuidParameter = new SqlParameter("@RowGuid", System.Data.SqlDbType.UniqueIdentifier);
                GuidParameter.Direction = System.Data.ParameterDirection.Output;
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.Add(GuidParameter);
                rowId =  (long)sqlCommand.ExecuteScalar();
                
            }
            return rowId;
        }

        public void UpdateSubmittedTax(Data.Models.SubmittedTaxModel submittedTaxModel)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string query = $"UPDATE master.SubmittedTax SET PostalCodeId = {submittedTaxModel.PostalCodeId}, Amount = {submittedTaxModel.Amount.ToString().Replace(",", ".")}, Updated = getdate(), Year = {submittedTaxModel.Year} WHERE RowGuid = '{submittedTaxModel.RowGuid}'";
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.ExecuteNonQuery();
            }
        }

        public void DeleteSubmittedTaxByGuid(Guid rowGuid)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string query = $"DELETE FROM master.SubmittedTax WHERE RowGuid = '{rowGuid}'";
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.ExecuteNonQuery();
            }
        }
    }
}
