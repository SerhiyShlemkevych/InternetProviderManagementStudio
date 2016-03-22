using Ipms.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Ipms.Repositories.Sql
{
    public class SqlConnectedHouseRepository : IConnectedHouseRepository
    {
        private const string UpdateQuery = "UPDATE tblConnectedHouse SET City = @city, Street = @street, House = @house, LastChangeInitiatorId = @administratorId WHERE Id = @id";
        private const string InserQuery = @"INSERT INTO tblConnectedHouse (City, Street, House, LastChangeInitiatorId) 
                                                VALUES(@city, @street, @house, @administratorId);
                                            SELECT SCOPE_IDENTITY();";
        private const string SelectAllQuery = "SELECT Id, City, Street, House FROM tblConnectedHouse";
        private const string SelectQuery = "SELECT Id, City, Street, House FROM tblConnectedHouse WHERE Id = @id";

        private string _connectionString;

        public SqlConnectedHouseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ConnectedHouseModel Get(int Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
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
                        return new ConnectedHouseModel()
                        {
                            Id = (int)reader["Id"],
                            City = (string)reader["City"],
                            Street = (string)reader["Street"],
                            House = (string)reader["House"]
                        };
                    }
                }
            }
        }

        public IEnumerable<ConnectedHouseModel> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(SelectAllQuery, connection))
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

        public int Insert(ConnectedHouseModel item, int administratorId)
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
                    command.Parameters.AddWithValue("@city", item.City);
                    command.Parameters.AddWithValue("@street", item.Street);
                    command.Parameters.AddWithValue("@house", item.House);
                    command.Parameters.AddWithValue("@administratorId", administratorId);
                    return Convert.ToInt32(command.ExecuteScalar());
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
                using (var command = new SqlCommand(UpdateQuery, connection))
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
    }
}
