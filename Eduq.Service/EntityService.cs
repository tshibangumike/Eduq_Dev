using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Eduq.Service
{
    public class EntityService
    {

        public static List<Dictionary<string, object>> QueryEntities()
        {
            var records = SharedService.ExecuteGetSqlStoredProcedure("[eduq].[Entity_queryall]", null);
            return records;
        }
        public static Dictionary<string, object> QueryEntityById(Guid entityId)
        {
            var records = SharedService.ExecuteGetSqlStoredProcedure("[eduq].[Entity_queryentitybyentityid]",
                new List<SqlParameter> { new SqlParameter("@entityId", entityId) });
            if (records != null && records.Count == 1) return records[0];
            return null;
        }

    }
}
