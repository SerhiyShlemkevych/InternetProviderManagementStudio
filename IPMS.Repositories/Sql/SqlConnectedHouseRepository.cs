using IPMS.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace IPMS.Repositories.Sql
{
    public class SqlConnectedHouseRepository : IConnectedHouseRepository
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

        public int Insert(ConnectedHouseModel item, int administratorId)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string insertQuery = "INSERT INTO tblConnectedHouse (City, Street, House, LastChangeInitiatorId) VALUES(@city, @street, @house, @administratorId);";
                string selectQuery = "SELECT SCOPE_IDENTITY()";
                {
                    using (var command = new SqlCommand(String.Format("{0}{1}", insertQuery, selectQuery), connection))
                    {
                        command.Parameters.AddWithValue("@city", item.City);
                        command.Parameters.AddWithValue("@street", item.Street);
                        command.Parameters.AddWithValue("@house", item.House);
                        command.Parameters.AddWithValue("@administratorId", administratorId);
                        return Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
        }

        public async Task<int> InsertAsync(ConnectedHouseModel item, int administratorId)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string insertQuery = "INSERT INTO tblConnectedHouse (City, Street, House, LastChangeInitiatorId) VALUES(@city, @street, @house, @administratorId)";
                string selectQuery = "SELECT SCOPE_IDENTITY()";

                using (var command = new SqlCommand(String.Format("{0}{1}", insertQuery, selectQuery), connection))
                {
                    command.Parameters.AddWithValue("@city", item.City);
                    command.Parameters.AddWithValue("@street", item.Street);
                    command.Parameters.AddWithValue("@house", item.House);
                    command.Parameters.AddWithValue("@administratorId", administratorId);
                    return Convert.ToInt32(await command.ExecuteScalarAsync());
                }
            }
        }

        public void Update(ConnectedHouseModel item, int administratorId)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE tblConnectedHouse SET City = @city, Street = @street, House = @house, LastChangeInitiatorId = @administratorId WHERE Id = @id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@city", item.City);
                    command.Parameters.AddWithValue("@street", item.Street);
                    command.Parameters.AddWithValue("@house", item.House);
                    command.Parameters.AddWithValue("@id", item.Id);
                    command.Parameters.AddWithValue("@administratorId", administratorId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public async Task UpdateAsync(ConnectedHouseModel item, int administratorId)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "UPDATE tblConnectedHouse SET City = @city, Street = @street, House = @house, LastChangeInitiatorId = @administratorId WHERE Id = @id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@city", item.City);
                    command.Parameters.AddWithValue("@street", item.Street);
                    command.Parameters.AddWithValue("@house", item.House);
                    command.Parameters.AddWithValue("@id", item.Id);
                    command.Parameters.AddWithValue("@administratorId", administratorId);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
