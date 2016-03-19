﻿using IPMS.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Repositories.Sql
{
    public class SqlAdministratorRepository : IAdministratorRepository
    {
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
                string query = "SELECT Id, Login, Forename, Surname FROM tblAdministrator WHERE Login = @login and Password = @password";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@login", login);
                    command.Parameters.AddWithValue("@password", password);
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
    }
}