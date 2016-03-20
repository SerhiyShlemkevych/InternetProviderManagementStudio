using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using IPMS.Models;

namespace IPMS.Repositories.Sql
{
    public class SqlTariffRepository : ITariffRepository
    {
        private const string UpdateQuery = @"UPDATE tblTariff SET Name = @name, Price = @price, UploadSpeed = @uploadSpeed, DownloadSpeed = @downloadSpeed, Archive = @isArchive, LastChangeInitiatorId = @administratorId WHERE Id = @id;";
        private const string InserQuery = @"INSERT INTO tblTariff (Name, Price, UploadSpeed, DownloadSpeed, Archive, LastChangeInitiatorId) 
                                                VALUES(@name, @price, @uploadSpeed, @downloadSpeed, @isArchive, @administratorId);
                                            SELECT SCOPE_IDENTITY();";
        private const string ArchiveQuery = "spArchiveTariff";
        private const string SelectAllQuery = "SELECT Id, Name, Price, UploadSpeed, DownloadSpeed, Archive FROM tblTariff";
        private const string SelectQuery = "SELECT Id, Name, Price, UploadSpeed, DownloadSpeed, Archive FROM tblTariff WHERE Id = @id";

        private string _connectionString;

        public SqlTariffRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Archive(TariffModel target, TariffModel substitute, int administratorId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(ArchiveQuery, connection))
                {
                    command.Parameters.AddWithValue("@targetTariffId", target.Id);
                    command.Parameters.AddWithValue("@substituteTariffId", substitute.Id);
                    command.Parameters.AddWithValue("@administratorId", administratorId);
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                }
            }
        }





        public TariffModel Get(int Id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(SelectQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            return null;
                        }

                        reader.Read();
                        return new TariffModel()
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                            DownloadSpeed = (int)reader["DownloadSpeed"],
                            UploadSpeed = (int)reader["UploadSpeed"],
                            Price = (decimal)reader["Price"],
                            IsArchive = (bool)reader["Archive"]
                        };
                    }
                }
            }
        }

        public IEnumerable<TariffModel> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(SelectAllQuery, connection))
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
                                IsArchive = (bool)reader["Archive"]
                            });
                        }
                        return result;
                    }
                }
            }
        }





        public int Insert(TariffModel item, int administratorId)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string insertQuery = @"INSERT INTO tblTariff (Name, Price, UploadSpeed, DownloadSpeed, Archive, LastChangeInitiatorId) VALUES(@name, @price, @uploadSpeed, @downloadSpeed, @isArchive, @administratorId);";
                string selectQuery = "SELECT SCOPE_IDENTITY();";

                    using (SqlCommand command = new SqlCommand(String.Format("{0}{1}", insertQuery, selectQuery), connection))
                    {
                        command.Parameters.AddWithValue("@name", item.Name);
                        command.Parameters.AddWithValue("@price", item.Price);
                        command.Parameters.AddWithValue("@uploadSpeed", item.UploadSpeed);
                        command.Parameters.AddWithValue("@downloadSpeed", item.UploadSpeed);
                        command.Parameters.AddWithValue("@isArchive", item.IsArchive);
                        command.Parameters.AddWithValue("@administratorId", administratorId);
                        return Convert.ToInt32(command.ExecuteScalar());
                    }

                
            }
        }

      
        public void Update(TariffModel item, int administratorId)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(UpdateQuery, connection))
                {
                    command.Parameters.AddWithValue("@name", item.Name);
                    command.Parameters.AddWithValue("@price", item.Price);
                    command.Parameters.AddWithValue("@uploadSpeed", item.UploadSpeed);
                    command.Parameters.AddWithValue("@downloadSpeed", item.UploadSpeed);
                    command.Parameters.AddWithValue("@id", item.Id);
                    command.Parameters.AddWithValue("@isArchive", item.IsArchive);
                    command.Parameters.AddWithValue("@administratorId", administratorId);
                    command.ExecuteNonQuery();
                }
            }
        }

      
    }
}
