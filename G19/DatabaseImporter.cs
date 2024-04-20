using System.Data;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Data.SqlClient;

namespace G19_ProductImport
{
    public class DatabaseImporter
    {
        private readonly string _connectionString;

        public DatabaseImporter(string connectionString)
        {            
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }
        
        public void ImportData(IEnumerable<Category> categories)
        {
            string connectionString = "Server=DESKTOP-ANU2LNI; Database=Products Task; Integrated Security=true; TrustServerCertificate=true";
            SqlConnection connection = new SqlConnection(connectionString);
            var command = connection.CreateCommand();

            command.CommandText = "sp_ImportProduct";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@CategoryName", SqlDbType.NVarChar);
            command.Parameters.Add("@CategoryIsActive", SqlDbType.Bit);
            command.Parameters.Add("@ProductCode", SqlDbType.NVarChar);
            command.Parameters.Add("@ProductName", SqlDbType.NVarChar);
            command.Parameters.Add("@ProductPrice", SqlDbType.Decimal);
            command.Parameters.Add("@ProductIsActive", SqlDbType.Bit);

            connection.Open();

            foreach (var category in categories)
            {
                foreach (var product in category.Products)
                {
                    command.Parameters["@CategoryName"].Value =  category.Name;
                    command.Parameters["@CategoryIsActive"].Value = category.IsActive;
                    command.Parameters["@ProductCode"].Value = product.Code;
                    command.Parameters["@ProductName"].Value = product.Name;
                    command.Parameters["@ProductPrice"].Value = product.Price;
                    command.Parameters["@ProductIsActive"].Value = product.IsActive;
                                        
                    command.ExecuteNonQuery();
                }               
            }
            connection.Close();
        }
    }
}