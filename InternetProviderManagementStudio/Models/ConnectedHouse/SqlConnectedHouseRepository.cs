using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetProviderManagementStudio.Models.ConnectedHouse
{
    class SqlConnectedHouseRepository : IConnectedHouseRepository
    {
        private string _connectionString;

        public SqlConnectedHouseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Delete(int Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "DELETE FROM tblConnectedHouse WHERE Id = @id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public async Task DeleteAsync(int Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM tblConnectedHouse WHERE Id = @id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public ConnectedHouseModel Get(int Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT Id, City, Street, House FROM tblConnectedHouse WHERE Id = @id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            return new ConnectedHouseModel()
                            {
                                Id = (int)reader["Id"],
                                City = (string)reader["City"],
                                Street = (string)reader["Street"],
                                House = (string)reader["House"]
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

        public IEnumerable<ConnectedHouseModel> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT Id, City, Street, House FROM tblConnectedHouse";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        List<ConnectedHouseModel> result = new List<ConnectedHouseModel>();
                        while (reader.Read())
                        {

                            result.Add(new ConnectedHouseModel()
                            {
                                Id = (int)reader["Id"],
                                City = (string)reader["City"],
                                Street = (string)reader["Street"],
                                House = (string)reader["House"]
                            });
                        }
                        return result;
                    }
                }
            }
        }

        public async Task<IEnumerable<ConnectedHouseModel>> GetAllAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, City, Street, House FROM tblConnectedHouse WHERE Id = @id";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        List<ConnectedHouseModel> result = new List<ConnectedHouseModel>();
                        while (await reader.ReadAsync())
                        {

                            result.Add(new ConnectedHouseModel()
                            {
                                Id = (int)reader["Id"],
                                City = (string)reader["City"],
                                Street = (string)reader["Street"],
                                House = (string)reader["House"]
                            });
                        }
                        return result;
                    }
                }
            }
        }

        public async Task<ConnectedHouseModel> GetAsync(int Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, City, Street, House FROM tblConnectedHouse WHERE Id = @id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            await reader.ReadAsync();
                            return new ConnectedHouseModel()
                            {
                                Id = (int)reader["Id"],
                                City = (string)reader["City"],
                                Street = (string)reader["Street"],
                                House = (string)reader["House"]
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

        public int Insert(ConnectedHouseModel item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string insertQuery = "INSERT INTO tblHouse (City, Street, House) VALUES(@city, @street, @house)";
                string selectQuery = "SELECT @@SCOPE_IDENTITY";
                int result = -1;
                using (var transaction = connection.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    using (var command = new SqlCommand(insertQuery, connection))
                    {
                        command.Transaction = transaction;
                        command.Parameters.AddWithValue("@city", item.City);
                        command.Parameters.AddWithValue("@street", item.Street);
                        command.Parameters.AddWithValue("@house", item.House);
                        command.ExecuteNonQuery();
                    }
                    using (var command = new SqlCommand(selectQuery, connection))
                    {
                        command.Transaction = transaction;
                        result = (int)command.ExecuteScalar();
                    }

                    transaction.Commit();
                    return result;
                }
            }
        }

        public async Task<int> InsertAsync(ConnectedHouseModel item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string insertQuery = "INSERT INTO tblHouse (City, Street, House) VALUES(@city, @street, @house)";
                string selectQuery = "SELECT @@SCOPE_IDENTITY";
                int result = -1;
                using (var transaction = connection.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    using (var command = new SqlCommand(insertQuery, connection))
                    {
                        command.Transaction = transaction;
                        command.Parameters.AddWithValue("@city", item.City);
                        command.Parameters.AddWithValue("@street", item.Street);
                        command.Parameters.AddWithValue("@house", item.House);
                        await command.ExecuteNonQueryAsync();
                    }
                    using (var command = new SqlCommand(selectQuery, connection))
                    {
                        command.Transaction = transaction;
                        result = (int)await command.ExecuteScalarAsync();
                    }

                    transaction.Commit();
                    return result;
                }
            }
        }

        public void Update(ConnectedHouseModel item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE tblHouse SET City = @city, Street = @street, House = @house WHERE Id = @id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@city", item.City);
                    command.Parameters.AddWithValue("@street", item.Street);
                    command.Parameters.AddWithValue("@house", item.House);
                    command.Parameters.AddWithValue("@id", item.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public async Task UpdateAsync(ConnectedHouseModel item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "UPDATE tblHouse SET City = @city, Street = @street, House = @house WHERE Id = @id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@city", item.City);
                    command.Parameters.AddWithValue("@street", item.Street);
                    command.Parameters.AddWithValue("@house", item.House);
                    command.Parameters.AddWithValue("@id", item.Id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
