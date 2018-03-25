using Eduq.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eduq.Web.Components.FieldType
{
    public class FieldTypeController : Controller
    {
        
        public JsonResult GetFieldTypes()
        {
            try
            {
                var records = FieldTypeService.QueryFieldTypes();
                return Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

    }
}