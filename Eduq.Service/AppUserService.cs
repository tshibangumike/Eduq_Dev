using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduq.Service
{
    public class AppUserService
    {

        public static List<Dictionary<string, object>> QueryAppUsers()
        {
            var records = SharedService.ExecuteGetSqlStoredProcedure("[eduq].[Appuser_queryall]", null);
            return records;
        }
        public static Dictionary<string, object> QueryAppUserById(Guid appUserId)
        {
            var records = SharedService.ExecuteGetSqlStoredProcedure("[eduq].[Appuser_querybyid]",
                 new List<SqlParameter> { new SqlParameter("@appUserId", appUserId) });
            if (records != null && records.Count == 1) return records[0];
            return null;
        }

    }
}
