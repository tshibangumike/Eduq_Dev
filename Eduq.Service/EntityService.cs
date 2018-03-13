using Eduq.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduq.Service
{
    public class EntityService
    {
        public static List<Dictionary<string, object>> QueryEntities()
        {
            var entities = SharedService.ExecuteGetSqlStoredProcedure("[eduq].[Entity_queryall]", null);
            return entities;
        }
    }
}
