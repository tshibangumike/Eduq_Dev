using Eduq.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eduq.Web.Components.AppUser
{
    public class AppUserController : Controller
    {
        
        public JsonResult GetAppUsers()
        {
            var records = AppUserService.QueryAppUsers();
            return Json(records, JsonRequestBehavior.AllowGet);
        }

    }
}