using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Eduq.Service
{
    public class SharedService
    {
        private static readonly string ConnectionString =
            ConfigurationManager.ConnectionStrings["eduqContext"].ConnectionString;

        public static void ExecutePostSqlStoredProcedure(string storedProcedureName, List<SqlParameter> listSqlParameter)
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                try
                {
                    var command = new SqlCommand
                    {
                        CommandText = storedProcedureName,
                        Connection = sqlConnection,
                        CommandType = CommandType.StoredProcedure
                    };
                    if (listSqlParameter != null)
                    {
                        foreach (var parameter in listSqlParameter)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    var result = command.ExecuteNonQuery();
                    Console.WriteLine(result);
                }
                catch (Exception ex)
                {
                }
            }
        }

        public static List<Dictionary<string, object>> ExecuteGetSqlStoredProcedure(string sqlQuery, List<SqlParameter> listSqlParameter)
        {
            var dataTableResult = new DataTable();
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                try
                {
                    var command = new SqlCommand
                    {
                        CommandText = sqlQuery,
                        Connection = sqlConnection,
                        CommandType = CommandType.StoredProcedure
                    };
                    if (listSqlParameter != null)
                    {
                        foreach (var parameter in listSqlParameter)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    var adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTableResult);
                    var records = (from DataRow dr in dataTableResult.Rows
                                   select
                                       dataTableResult.Columns.Cast<DataColumn>()
                                           .ToDictionary(col => col.ColumnName, col => dr[col])).ToList();
                    return records;
                }
                catch (Exception ex)
                {
                    //Log.Error(ex.Message);
                }
            }
            return null;
        }
    }
}
