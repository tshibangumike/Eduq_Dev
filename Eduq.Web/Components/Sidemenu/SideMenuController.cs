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
        public ActionResult GetParentSideMenuItems()
        {
            var parentSideMenuItems = SideMenuService.QueryParentSideMenuItems();
            return Json(parentSideMenuItems, JsonRequestBehavior.AllowGet);
        }

    }
}