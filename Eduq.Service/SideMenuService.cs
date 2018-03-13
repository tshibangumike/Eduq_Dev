using System.Collections.Generic;

namespace Eduq.Service
{
    public class SideMenuService
    {
        public static List<Dictionary<string, object>> QuerySideMenuItems()
        {
            var records = SharedService.ExecuteGetSqlStoredProcedure("[eduq].[Sidemenuitem_queryall]", null);
            return records;
        }
    }
}
