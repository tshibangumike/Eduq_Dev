using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eduq.Service;

namespace Eduq.Web.Components.Sidemenu
{
    public class SideMenuController : Controller
    {
        
        public ActionResult GetSideMenuItems()
        {
            var sideMenuItems = SideMenuService.QuerySideMenuItems();
            return Json(sideMenuItems, JsonRequestBehavior.AllowGet);
        }

    }
}