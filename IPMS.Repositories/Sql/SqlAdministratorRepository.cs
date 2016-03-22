using Ipms.Models;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace Ipms.Repositories.Sql
{
    public class SqlAdministratorRepository : IAdministratorRepository
    {
        private const string SelectQuery = "SELECT Id, Login, Forename, Surname FROM tblAdministrator WHERE Login = @login and Password = @password";

        private string _connectionString;

        public SqlAdministratorRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public AdministratorModel Authenticate(string login, string password)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(SelectQuery, connection))
                {
                    command.Parameters.AddWithValue("@login", login);
                    command.Parameters.AddWithValue("@password", Encode(password));
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            return null;
                        }
                        reader.Read();
                        return new AdministratorModel()
                        {
                            Id = (int)reader["Id"],
                            Login = (string)reader["Login"],
                            Forename = (string)reader["Forename"],
                            Surname = (string)reader["Surname"]
                        };
                    }
                }
            }
        }

        private string Encode(string text)
        {
            using (MD5 md5 = new MD5CryptoServiceProvider())
            {
                //compute hash from the bytes of text
                md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

                //get hash result after compute it
                byte[] result = md5.Hash;

                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    //change it into 2 hexadecimal digits
                    //for each byte
                    stringBuilder.Append(result[i].ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }
    }
}
