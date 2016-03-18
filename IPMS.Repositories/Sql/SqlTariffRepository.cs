using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using IPMS.Models;

namespace IPMS.Repositories.Sql
{
    public class SqlTariffRepository : ITariffRepository
    {
        private string _connectionString;

        public SqlTariffRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Archive(TariffModel target, TariffModel substitute)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "spArchiveTariff";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@targetTariffId", target.Id);
                    command.Parameters.AddWithValue("@substituteTariffId", substitute.Id);
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                }
            }
        }

        public async Task ArchiveAsync(TariffModel target, TariffModel substitute)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "ArchiveTariff";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@targetTariffId", target.Id);
                    command.Parameters.AddWithValue("@@substituteTariffId", substitute.Id);
                    command.CommandType = CommandType.StoredProcedure;
                    await command.ExecuteNonQueryAsync();
                }
            }
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
                string query = "SELECT Id, Name, Price, UploadSpeed, DownloadSpeed, IsArchive FROM tblTariff WHERE Id = @id";
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
                                Price = (decimal)reader["Price"],
                                IsArchive = (bool)reader["IsArchive"]
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
                string query = "SELECT Id, Name, Price, UploadSpeed, DownloadSpeed, IsArchive FROM tblTariff";
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
                                Price = (decimal)reader["Price"],
                                IsArchive = (bool)reader["IsArchive"]
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
                string query = "SELECT Id, Name, Price, UploadSpeed, DownloadSpeed, IsArchive FROM tblTariff";
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
                                Price = (decimal)reader["Price"],
                                IsArchive = (bool)reader["IsArchive"]
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
                string query = "SELECT Id, Name, Price, UploadSpeed, DownloadSpeed, IsArchive FROM tblTariff WHERE Id = @id";
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
                                Price = (decimal)reader["Price"],
                                IsArchive = (bool)reader["IsArchive"]
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
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string insertQuery = @"INSERT INTO tblTariff (Name, Price, UploadSpeed, DownloadSpeed, IsArchive) VALUES(@name, @price, @uploadSpeed, @downloadSpeed, @isArchive);";
                string selectQuery = "SELECT @@IDENTITY;";
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
                        command.Parameters.AddWithValue("@isArchive", item.IsArchive);
                        command.ExecuteNonQuery();
                    }
                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        command.Transaction = transaction;
                        result = Convert.ToInt32(command.ExecuteScalar());
                    }

                    transaction.Commit();
                    return result;
                }
            }
        }

        public async Task<int> InsertAsync(TariffModel item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string insertQuery = @"INSERT INTO tblTariff (Name, Price, UploadSpeed, DownloadSpeed, IsArchive) VALUES(@name, @price, @uploadSpeed, @downloadSpeed, @isArchive);";
                string selectQuery = "SELECT @@IDENTITY;";
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
                        command.Parameters.AddWithValue("@isArchive", item.IsArchive);
                        await command.ExecuteNonQueryAsync();
                    }
                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        command.Transaction = transaction;
                        result = Convert.ToInt32(await command.ExecuteScalarAsync());
                    }

                    transaction.Commit();
                    return result;
                }
            }
        }

        public void Update(TariffModel item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"UPDATE tblTariff SET Name = @name, Price = @price, UploadSpeed = @uploadSpeed, DownloadSpeed = @downloadSpeed, IsArchive = @isArchive WHERE Id = @id;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", item.Name);
                    command.Parameters.AddWithValue("@price", item.Price);
                    command.Parameters.AddWithValue("@uploadSpeed", item.UploadSpeed);
                    command.Parameters.AddWithValue("@downloadSpeed", item.UploadSpeed);
                    command.Parameters.AddWithValue("@id", item.Id);
                    command.Parameters.AddWithValue("@isArchive", item.IsArchive);
                    command.ExecuteNonQuery();
                }
            }
        }

        public async Task UpdateAsync(TariffModel item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"UPDATE tblTariff SET Name = @name, Price = @price, UploadSpeed = uploadSpeed, DownloadSpeed = downloadSpeed, IsArchive=@isArchive WHERE Id = @id;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", item.Name);
                    command.Parameters.AddWithValue("@price", item.Price);
                    command.Parameters.AddWithValue("@uploadSpeed", item.UploadSpeed);
                    command.Parameters.AddWithValue("@downloadSpeed", item.UploadSpeed);
                    command.Parameters.AddWithValue("@id", item.Id);
                    command.Parameters.AddWithValue("@isArchive", item.IsArchive);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
