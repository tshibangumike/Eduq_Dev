using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduq.Service
{
    public class FieldTypeService
    {
        public static List<Dictionary<string, object>> QueryFieldTypes()
        {
            var records = SharedService.ExecuteGetSqlStoredProcedure("[eduq].[Fieldtype_queryall]", null);
            return records;
        }
        public static Dictionary<string, object> QueryFieldTypeByName(string fieldTypeName)
        {
            var records = SharedService.ExecuteGetSqlStoredProcedure("[eduq].[Fieldtype_querybyname]",
                new List<SqlParameter> { new SqlParameter("@fieldTypeName", fieldTypeName) });
            if (records != null && records.Count == 1) return records[0];
            return null;
        }
    }
}
