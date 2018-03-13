using System;
using System.Web.Mvc;
using Eduq.Service;
using Eduq.Web.Components.Base;

namespace Eduq.Web.Components.Entity
{
    public class EntityController : BaseController
    {
        public JsonResult GetEntities()
        {
            try
            {
                var records = EntityService.QueryEntities();
                return Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}