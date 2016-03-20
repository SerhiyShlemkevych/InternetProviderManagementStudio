using IPMS.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace IPMS.Repositories.Sql
{
    public class SqlCustomerRepository : ICustomerRepository
    {
        private const string UpdateQuery = @"UPDATE tblCustomer SET Forename = @forename, Surname = @surname, HouseId = @houseId, Flat = @flat, TariffId = @tariffId, Balance = @balance, [State] = @state, MacAddress = @macAddress, IpAddress = @ipAddress, LastChargedDate = @lastChargedDate, LastChangeInitiatorId = @administratorId WHERE Id = @id;";
        private const string InserQuery = @"INSERT INTO tblCustomer (Forename, Surname, HouseId, Flat, TariffId, Balance, [State], MacAddress, IpAddress, LastChargedDate, LastChangeInitiatorId) 
                                                VALUES(@forename, @surname, @houseId, @flat, @tariffId, @balance, @state, @macAddress, @ipAddress, @lastChargedDate, @administratorId);
                                            SELECT SCOPE_IDENTITY()";
        private const string GetChargeQuery = "spGetCharge";
        private const string SelectAllQuery = "SELECT Id, ForeName, Surname, HouseId, Flat, TariffId, Balance, [State], MacAddress, IpAddress, LastChargedDate FROM tblCustomer";
        private const string SelectQuery = "SELECT Id, ForeName, Surname, HouseId, Flat, TariffId, Balance, [State], MacAddress, IpAddress, LastChargedDate FROM tblCustomer WHERE Id = @id";
        private const string SelectctBalanceLogForCustomerQuery = "SELECT Id, CustomerId, Amount, Balance, [Date], [Description] FROM tblBalanceLog WHERE CustomerId = @id";
        private const string SelectctBalanceLogQuery = "SELECT Id, CustomerId, Amount, Balance, [Date], [Description] FROM tblBalanceLog";

        private string _connectionString;

        public SqlCustomerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public CustomerModel Get(int Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                List<BalanceLogItemModel> balanceLog = new List<BalanceLogItemModel>();

                using (var command = new SqlCommand(SelectctBalanceLogForCustomerQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);
                    using (var reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            balanceLog.Add(new BalanceLogItemModel()
                            {
                                Id = (int)reader["Id"],
                                CustomerId = (int)reader["CustomerId"],
                                Amount = (decimal)reader["Amount"],
                                Balance = (decimal)reader["Balance"],
                                Date = (DateTime)reader["Date"],
                                Description = (string)reader["Description"]
                            });
                        }
                    }
                }

                using (var command = new SqlCommand(SelectQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            return null;
                        }

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
                            State = (CustomerState)reader["State"],
                            MacAddress = (string)reader["MacAddress"],
                            IpAddress = (string)reader["IpAddress"],
                            LastChargedDate = (DateTime)reader["LastChargedDate"],
                            BalanceLog = balanceLog
                        };

                    }
                }
            }
        }

        public IEnumerable<CustomerModel> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                List<BalanceLogItemModel> balanceLog = new List<BalanceLogItemModel>();

                using (var command = new SqlCommand(SelectctBalanceLogQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            balanceLog.Add(new BalanceLogItemModel()
                            {
                                Id = (int)reader["Id"],
                                CustomerId = (int)reader["CustomerId"],
                                Amount = (decimal)reader["Amount"],
                                Balance = (decimal)reader["Balance"],
                                Date = (DateTime)reader["Date"],
                                Description = (string)reader["Description"]
                            });
                        }
                    }
                }

                using (var command = new SqlCommand(SelectAllQuery, connection))
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
                                State = (CustomerState)reader["State"],
                                MacAddress = (string)reader["MacAddress"],
                                IpAddress = (string)reader["IpAddress"],
                                LastChargedDate = (DateTime)reader["LastChargedDate"],
                                BalanceLog = balanceLog.Where(item=>item.CustomerId == (int)reader["Id"]).ToList()
                            });
                        }
                        return result;
                    }
                }
            }
        }

        public void GetCharge(int administratorId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(GetChargeQuery, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@administratorId", administratorId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public int Insert(CustomerModel item, int administratorId)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(InserQuery, connection))
                {
                    command.Parameters.AddWithValue("@administratorId", administratorId);
                    command.Parameters.AddWithValue("@forename", item.Forename);
                    command.Parameters.AddWithValue("@surmane", item.Surname);
                    command.Parameters.AddWithValue("@houseId", item.HouseId);
                    command.Parameters.AddWithValue("@flat", item.Flat);
                    command.Parameters.AddWithValue("@tariffId", item.TariffId);
                    command.Parameters.AddWithValue("@balance", item.Balance);
                    command.Parameters.AddWithValue("@state", 1);
                    command.Parameters.AddWithValue("macAddress", item.MacAddress);
                    command.Parameters.AddWithValue("ipAddress", item.IpAddress);
                    command.Parameters.AddWithValue("@lastChargedDate", item.LastChargedDate);
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public void Update(CustomerModel item, int administratorId)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(UpdateQuery, connection))
                {
                    command.Parameters.AddWithValue("@administratorId", administratorId);
                    command.Parameters.AddWithValue("@forename", item.Forename);
                    command.Parameters.AddWithValue("@surname", item.Surname);
                    command.Parameters.AddWithValue("@houseId", item.HouseId);
                    command.Parameters.AddWithValue("@flat", item.Flat);
                    command.Parameters.AddWithValue("@tariffId", item.TariffId);
                    command.Parameters.AddWithValue("@balance", item.Balance);
                    command.Parameters.AddWithValue("@state", item.State);
                    command.Parameters.AddWithValue("@macAddress", item.MacAddress);
                    command.Parameters.AddWithValue("@ipAddress", item.IpAddress);
                    command.Parameters.AddWithValue("@lastChargedDate", item.LastChargedDate);
                    command.Parameters.AddWithValue("@id", item.Id);
                    command.ExecuteNonQuery();
                }
            }
        }


    }
}
