using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetProviderManagementStudio.Models.Customer
{
    class SqlCustomerRepository : ICustomerRepository
    {
        private string _connectionString;

        public SqlCustomerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Delete(int Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM tblCustomer WHERE Id = @id";
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
                string query = "DELETE FROM tblCustomer WHERE Id = @id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public CustomerModel Get(int Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT Id, ForeName, Surname, HouseId, Flat, TariffId, Balance, Status, MacAddress, IpAddress, LastChargedDate FROM tblCustomer WHERE Id = @id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            return new CustomerModel()
                            {
                                Id = (int)reader["Id"],
                                Forename = (string)reader["Forename"],
                                Surname = (string)reader["Surname"],
                                TariffId = (int)reader["TariffId"],
                                HouseId = (int)reader["HouseId"],
                                Flat = (string)reader["Flat"],
                                Balance = (decimal)reader["Balance"],
                                Status = (int)reader["Status"],
                                MacAddress = (string)reader["MacAddress"],
                                IpAddress = (string)reader["IpAddress"],
                                LastChargedDate = (DateTime)reader["LastChargedDate"]
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

        public IEnumerable<CustomerModel> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT Id, ForeName, Surname, HouseId, Flat, TariffId, Balance, Status, MacAddress, IpAddress, LastChargedDate FROM tblCustomer";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        List<CustomerModel> result = new List<CustomerModel>();
                        while (reader.Read())
                        {
                            result.Add(new CustomerModel()
                            {
                                Id = (int)reader["Id"],
                                Forename = (string)reader["Forename"],
                                Surname = (string)reader["Surname"],
                                TariffId = (int)reader["TariffId"],
                                HouseId = (int)reader["HouseId"],
                                Flat = (string)reader["Flat"],
                                Balance = (decimal)reader["Balance"],
                                Status = (int)reader["Status"],
                                MacAddress = (string)reader["MacAddress"],
                                IpAddress = (string)reader["IpAddress"],
                                LastChargedDate = (DateTime)reader["LastChargedDate"]
                            });
                        }
                        return result;
                    }
                }
            }
        }

        public async Task<IEnumerable<CustomerModel>> GetAllAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT Id, ForeName, Surname, HouseId, Flat, TariffId, Balance, Status, MacAddress, IpAddress, LastChargedDate FROM tblCustomer";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        List<CustomerModel> result = new List<CustomerModel>();
                        while (await reader.ReadAsync())
                        {
                            result.Add(new CustomerModel()
                            {
                                Id = (int)reader["Id"],
                                Forename = (string)reader["Forename"],
                                Surname = (string)reader["Surname"],
                                TariffId = (int)reader["TariffId"],
                                HouseId = (int)reader["HouseId"],
                                Flat = (string)reader["Flat"],
                                Balance = (decimal)reader["Balance"],
                                Status = (int)reader["Status"],
                                MacAddress = (string)reader["MacAddress"],
                                IpAddress = (string)reader["IpAddress"],
                                LastChargedDate = (DateTime)reader["LastChargedDate"]
                            });
                        }
                        return result;
                    }
                }
            }
        }

        public async Task<CustomerModel> GetAsync(int Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT Id, ForeName, Surname, HouseId, Flat, TariffId, Balance, Status, MacAddress, IpAddress, LastChargedDate FROM tblCustomer WHERE Id = @id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            await reader.ReadAsync();
                            return new CustomerModel()
                            {
                                Id = (int)reader["Id"],
                                Forename = (string)reader["Forename"],
                                Surname = (string)reader["Surname"],
                                TariffId = (int)reader["TariffId"],
                                HouseId = (int)reader["HouseId"],
                                Flat = (string)reader["Flat"],
                                Balance = (decimal)reader["Balance"],
                                Status = (int)reader["Status"],
                                MacAddress = (string)reader["MacAddress"],
                                IpAddress = (string)reader["IpAddress"],
                                LastChargedDate = (DateTime)reader["LastChargedDate"]
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

        public int Insert(CustomerModel item)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string insertQuery = @"INSERT INTO tblCustomer (Forename, Surname, HouseId, Flat, TariffId, Balance, Status, MacAddress, IpAddress, LastChargedDate) 
                                     VALUES(@forename, @surname, @houseId, @flat, @tariffId, @balance, @status, @macAddress, @ipAddress, @lastChargedDate;";
                string selectQuery = "SELCT @@SCOPE_IDENTITY;";
                int result = -1;
                using (var transaction = connection.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {

                    using (var command = new SqlCommand(insertQuery, connection))
                    {
                        command.Transaction = transaction;
                        command.Parameters.AddWithValue("@forename", item.Forename);
                        command.Parameters.AddWithValue("@surmane", item.Surname);
                        command.Parameters.AddWithValue("@houseId", item.HouseId);
                        command.Parameters.AddWithValue("@flat", item.Flat);
                        command.Parameters.AddWithValue("@tariffId", item.TariffId);
                        command.Parameters.AddWithValue("@balance", item.Balance);
                        command.Parameters.AddWithValue("@status", item.Balance);
                        command.Parameters.AddWithValue("macAddress", item.MacAddress);
                        command.Parameters.AddWithValue("ipAddress", item.IpAddress);
                        command.Parameters.AddWithValue("@lastChargedDate", item.LastChargedDate);
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

        public async Task<int> InsertAsync(CustomerModel item)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string insertQuery = @"INSERT INTO tblCustomer (Forename, Surname, HouseId, Flat, TariffId, Balance, Status, MacAddress, IpAddress, LastChargedDate) 
                                     VALUES(@forename, @surname, @houseId, @flat, @tariffId, @balance, @status, @macAddress, @ipAddress, @lastChargedDate;";
                string selectQuery = "SELCT @@SCOPE_IDENTITY;";
                int result = -1;
                using (var transaction = connection.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {

                    using (var command = new SqlCommand(insertQuery, connection))
                    {
                        command.Transaction = transaction;
                        command.Parameters.AddWithValue("@forename", item.Forename);
                        command.Parameters.AddWithValue("@surmane", item.Surname);
                        command.Parameters.AddWithValue("@houseId", item.HouseId);
                        command.Parameters.AddWithValue("@flat", item.Flat);
                        command.Parameters.AddWithValue("@tariffId", item.TariffId);
                        command.Parameters.AddWithValue("@balance", item.Balance);
                        command.Parameters.AddWithValue("@status", item.Balance);
                        command.Parameters.AddWithValue("macAddress", item.MacAddress);
                        command.Parameters.AddWithValue("ipAddress", item.IpAddress);
                        command.Parameters.AddWithValue("@lastChargedDate", item.LastChargedDate);
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

        public void Update(CustomerModel item)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"UPDATE tblCustomer SET Forename = @forename, Surname = @surname, HouseId = @houseId, Flat = @flat, TariffId = @tariffId, Balance = @balance, Status = @status, MacAddress = @macAddress, IpAddress = @ipAddress, LastChargedDate = @lastChargedDate WHERE Id = @id;";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@forename", item.Forename);
                    command.Parameters.AddWithValue("@surmane", item.Surname);
                    command.Parameters.AddWithValue("@houseId", item.HouseId);
                    command.Parameters.AddWithValue("@flat", item.Flat);
                    command.Parameters.AddWithValue("@tariffId", item.TariffId);
                    command.Parameters.AddWithValue("@balance", item.Balance);
                    command.Parameters.AddWithValue("@status", item.Status);
                    command.Parameters.AddWithValue("@macAddress", item.MacAddress);
                    command.Parameters.AddWithValue("@ipAddress", item.IpAddress);
                    command.Parameters.AddWithValue("@lastChargedDate", item.LastChargedDate);
                    command.ExecuteNonQuery();
                }
            }
        }

        public async Task UpdateAsync(CustomerModel item)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"UPDATE tblCustomer SET Forename = @forename, Surname = @surname, HouseId = @houseId, Flat = @flat, TariffId = @tariffId, Balance = @balance, Status = @status, MacAddress = @macAddress, IpAddress = @ipAddress, LastChargedDate = @lastChargedDate WHERE Id = @id;";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@forename", item.Forename);
                    command.Parameters.AddWithValue("@surmane", item.Surname);
                    command.Parameters.AddWithValue("@houseId", item.HouseId);
                    command.Parameters.AddWithValue("@flat", item.Flat);
                    command.Parameters.AddWithValue("@tariffId", item.TariffId);
                    command.Parameters.AddWithValue("@balance", item.Balance);
                    command.Parameters.AddWithValue("@status", item.Status);
                    command.Parameters.AddWithValue("@macAddress", item.MacAddress);
                    command.Parameters.AddWithValue("@ipAddress", item.IpAddress);
                    command.Parameters.AddWithValue("@lastChargedDate", item.LastChargedDate);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
