using System.Collections.Generic;
using System.Data.SqlClient;

namespace Eduq.Service
{
    public class SqlService
    {

        public static bool CreateSqlTable(string tableName)
        {
            var returnValue = SharedService.ExecutePostSqlStoredProcedure("[eduq].[Sql_createsqltable]",
                    new List<SqlParameter>
                    {
                            new SqlParameter("@tableName", tableName)
                    });
            return returnValue == 1;
        }

        public static bool DeleteSqlTable(string tableName)
        {
            var returnValue = SharedService.ExecutePostSqlStoredProcedure("[eduq].[Sql_deletesqltable]",
                    new List<SqlParameter>
                    {
                            new SqlParameter("@tableName", tableName)
                    });
            return returnValue == 1;
        }

        public static bool CreateSqlTableColumn(string tableName, string columnName, string dataTypeName, string isRequired)
        {
            var returnValue = SharedService.ExecutePostSqlStoredProcedure("[eduq].[Sql_createsqltablecolumn]",
                    new List<SqlParameter>
                    {
                            new SqlParameter("@tableName", tableName),
                            new SqlParameter("@columnName", columnName),
                            new SqlParameter("@columnDataTypeName", dataTypeName),
                            new SqlParameter("@isRequired", isRequired)
                    });
            return returnValue == 1;
        }

        public static bool CreateSqlForeignKeyConstraint(string tableName, string foreignKeyTableName, string columnName)
        {
            var returnValue = SharedService.ExecutePostSqlStoredProcedure("[eduq].[Sql_createsqlforeignkeycontraint]",
                    new List<SqlParameter>
                    {
                            new SqlParameter("@tableName", tableName),
                            new SqlParameter("@foreignKeyTableName", foreignKeyTableName),
                            new SqlParameter("@columnName", columnName)
                    });
            return returnValue == 1;
        }

        public static bool DeleteSqlTableColumn(string tableName, string columnName)
        {
            var returnValue = SharedService.ExecutePostSqlStoredProcedure("[eduq].[Sql_deletesqltablecolumn]",
                    new List<SqlParameter>
                    {
                            new SqlParameter("@tableName", tableName),
                            new SqlParameter("@columnName", columnName)
                    });
            return returnValue == 1;
        }

    }
}
