using System.Data;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Data.SqlClient;

namespace G19_ProductImport
{
    public class DatabaseImporter
    {                
        public  void ImportData(IEnumerable<Category> categories)
        {
            string connectionString = "Server=DESKTOP-ANU2LNI; Database=Products Task; Integrated Security=true; TrustServerCertificate=true";
            Database database = new Database(connectionString);
            //SqlConnection connection = new SqlConnection(connectionString);
            //SqlCommand command = database.GetCommand(string commandText, CommandType commandType,params SqlParameter[] parametrs);
            var command = database.OpenConnection().CreateCommand();                     

            command.CommandText = "sp_ImportProduct";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@CategoryName", SqlDbType.NVarChar);
            command.Parameters.Add("@CategoryIsActive", SqlDbType.Bit);
            command.Parameters.Add("@ProductCode", SqlDbType.NVarChar);
            command.Parameters.Add("@ProductName", SqlDbType.NVarChar);
            command.Parameters.Add("@ProductPrice", SqlDbType.Decimal);
            command.Parameters.Add("@ProductIsActive", SqlDbType.Bit);

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
            database.CloseConnection();            
        }
    }
}