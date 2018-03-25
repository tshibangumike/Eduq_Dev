using Eduq.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduq.Service
{
    public class FieldService
    {

        public static List<Dictionary<string, object>> QueryFields()
        {
            var records = SharedService.ExecuteGetSqlStoredProcedure("[eduq].[Field_queryall]", null);
            return records;
        }
        public static Dictionary<string, object> QueryFieldById(Guid fieldId)
        {
            var records = SharedService.ExecuteGetSqlStoredProcedure("[eduq].[Field_queryfieldbyfieldid]",
                new List<SqlParameter> { new SqlParameter("@fieldId", fieldId) });
            if (records != null && records.Count == 1) return records[0];
            return null;
        }
        public static List<Dictionary<string, object>> QueryFieldsByEntityId(Guid entityId)
        {
            var records = SharedService.ExecuteGetSqlStoredProcedure("[eduq].[Field_queryfieldsbyentityid]",
                new List<SqlParameter> { new SqlParameter("@entityId", entityId) });
            return records;
        }
        public static Guid CreateField(Guid id, string name, string displayName, string description, string defaultValue, Guid entityId, int fieldTypeId, bool isRequired, int stateId)
        {
            var returnValue = SharedService.ExecutePostSqlStoredProcedure("[eduq].[Field_create]",
                new List<SqlParameter> {
                    new SqlParameter("@id", id),
                    new SqlParameter("@name", name),
                    new SqlParameter("@displayName", displayName),
                    new SqlParameter("@description", description),
                    new SqlParameter("@defaultValue", defaultValue),
                    new SqlParameter("@entityId", entityId),
                    new SqlParameter("@fieldTypeId", fieldTypeId),
                    new SqlParameter("@isRequired", isRequired),
                    new SqlParameter("@stateId", stateId)
                });
            if(returnValue != 1)
            {
                return Guid.Empty;
            }
            var newlyCreatedField = QueryFieldById(id);
            if(newlyCreatedField == null || newlyCreatedField.Count == 0)
            {
                return Guid.Empty;
            }
            var newlyCreatedFieldId = newlyCreatedField.FirstOrDefault(x => x.Key == "Id");
            if(newlyCreatedField == null)
            {
                return Guid.Empty;
            }
            return Guid.Parse(newlyCreatedFieldId.Value.ToString());
        }
    }
}
