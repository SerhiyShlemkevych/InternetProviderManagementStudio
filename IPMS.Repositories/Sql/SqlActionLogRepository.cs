using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ipms.Models;
using System.Data.SqlClient;

namespace Ipms.Repositories.Sql
{
    public class SqlActionLogRepository : IActionLogRepository
    {
        private const string SelectAllLogItemsQuery = "SELECT Id, [Action], [Target], AdministratorId, [Date], AffectedRowIds FROM tblActionLog";
        private const string SelectAllAdministratorsQuery = "SELECT Id, Login, Forename, Surname, [Disabled] FROM tblAdministrator";

        private string _connectionString;

        public SqlActionLogRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<ActionLogItemModel> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                List<AdministratorModel> administrators = new List<AdministratorModel>();

                using (var command = new SqlCommand(SelectAllAdministratorsQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            administrators.Add(new AdministratorModel()
                            {
                                Id = (int)reader["Id"],
                                Login = (string)reader["Login"],
                                Forename = (string)reader["Forename"],
                                Surname = (string)reader["Surname"],
                                IsDisabled = (bool)reader["Disabled"]
                            });
                        }
                    }
                }

                using (var command = new SqlCommand(SelectAllLogItemsQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<ActionLogItemModel> result = new List<ActionLogItemModel>();

                        while (reader.Read())
                        {
                            result.Add(new ActionLogItemModel()
                            {
                                Id = (int)reader["Id"],
                                Action = (string)reader["Action"],
                                Target = (string)reader["Target"],
                                Date = (DateTime)reader["Date"],
                                AffectedRowIds = reader["AffectedRowIds"] == DBNull.Value ? string.Empty : (string)reader["AffectedRowIds"],
                                Administrator = administrators.Where(a => a.Id == (int)reader["AdministratorId"]).First()
                            });
                        }

                        return result;
                    }
                }
            }
        }
    }
}
