using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace InternetProviderManagementStudio.Models.Tariff
{
    public class SqlTariffRepository : ITariffRepository
    {
        private string _connectionString;

        public SqlTariffRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Delete(int Id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "REMOVE FROM tblTariff WHERE Id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public async Task DeleteAsync(int Id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "REMOVE FROM tblTariff WHERE Id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public TariffModel Get(int Id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT Id, Name, Price, UploadSpeed, DownloadSpeed FROM tblTariff WHERE Id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            return new TariffModel()
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                DownloadSpeed = (int)reader["DownloadSpeed"],
                                UploadSpeed = (int)reader["UploadSpeed"],
                                Price = (decimal)reader["Price"]
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public IEnumerable<TariffModel> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT Id, Name, Price, UploadSpeed, DownloadSpeed FROM tblTariff";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        List<TariffModel> result = new List<TariffModel>();
                        while (reader.Read())
                        {
                            result.Add(new TariffModel()
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                DownloadSpeed = (int)reader["DownloadSpeed"],
                                UploadSpeed = (int)reader["UploadSpeed"],
                                Price = (decimal)reader["Price"]
                            });
                        }
                        return result;
                    }
                }
            }
        }

        public async Task<IEnumerable<TariffModel>> GetAllAsync()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT Id, Name, Price, UploadSpeed, DownloadSpeed FROM tblTariff";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        List<TariffModel> result = new List<TariffModel>();
                        while (await reader.ReadAsync())
                        {
                            result.Add(new TariffModel()
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                DownloadSpeed = (int)reader["DownloadSpeed"],
                                UploadSpeed = (int)reader["UploadSpeed"],
                                Price = (decimal)reader["Price"]
                            });
                        }
                        return result;
                    }
                }
            }
        }

        public async Task<TariffModel> GetAsync(int Id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT Id, Name, Price, UploadSpeed, DownloadSpeed FROM tblTariff WHERE Id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            await reader.ReadAsync();
                            return new TariffModel()
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                DownloadSpeed = (int)reader["DownloadSpeed"],
                                UploadSpeed = (int)reader["UploadSpeed"],
                                Price = (decimal)reader["Price"]
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public int Insert(TariffModel item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string insertQuery = @"INSERT INTO tblTariff (Name, Price, UploadSpeed, DownloadSpeed) VALUES(@name, @price, @uploadSpeed, @downloadSpeed);";
                string selectQuery = "SELECT @@SCOPE_IDENTITY;";
                int result = -1;

                using (var transaction = connection.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Transaction = transaction;
                        command.Parameters.AddWithValue("@name", item.Name);
                        command.Parameters.AddWithValue("@price", item.Price);
                        command.Parameters.AddWithValue("@uploadSpeed", item.UploadSpeed);
                        command.Parameters.AddWithValue("@downloadSpeed", item.UploadSpeed);
                        command.ExecuteNonQuery();
                    }
                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        command.Transaction = transaction;
                        result = (int)command.ExecuteScalar();
                    }

                    transaction.Commit();
                    return result;
                }
            }
        }

        public async Task<int> InsertAsync(TariffModel item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string insertQuery = @"INSERT INTO tblTariff (Name, Price, UploadSpeed, DownloadSpeed) VALUES(@name, @price, @uploadSpeed, @downloadSpeed);";
                string selectQuery = "SELECT @@SCOPE_IDENTITY;";
                int result = -1;

                using (var transaction = connection.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Transaction = transaction;
                        command.Parameters.AddWithValue("@name", item.Name);
                        command.Parameters.AddWithValue("@price", item.Price);
                        command.Parameters.AddWithValue("@uploadSpeed", item.UploadSpeed);
                        command.Parameters.AddWithValue("@downloadSpeed", item.UploadSpeed);
                        await command.ExecuteNonQueryAsync();
                    }
                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        command.Transaction = transaction;
                        result = (int)await command.ExecuteScalarAsync();
                    }

                    transaction.Commit();
                    return result;
                }
            }
        }

        public void Update(TariffModel item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"UPDATE tblTariff SET Name = @name, Price = @price, UploadSpeed = uploadSpeed, DownloadSpeed = downloadSpeed WHERE Id = @id;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", item.Name);
                    command.Parameters.AddWithValue("@price", item.Price);
                    command.Parameters.AddWithValue("@uploadSpeed", item.UploadSpeed);
                    command.Parameters.AddWithValue("@downloadSpeed", item.UploadSpeed);
                    command.Parameters.AddWithValue("@id", item.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public async Task UpdateAsync(TariffModel item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"UPDATE tblTariff SET Name = @name, Price = @price, UploadSpeed = uploadSpeed, DownloadSpeed = downloadSpeed WHERE Id = @id;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", item.Name);
                    command.Parameters.AddWithValue("@price", item.Price);
                    command.Parameters.AddWithValue("@uploadSpeed", item.UploadSpeed);
                    command.Parameters.AddWithValue("@downloadSpeed", item.UploadSpeed);
                    command.Parameters.AddWithValue("@id", item.Id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
