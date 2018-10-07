using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hukuk3K.UI.Areas.Portal.Models.Attributes
{
    public class PortalLoginControl : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["portal"] == null)
            {
                filterContext.HttpContext.Response.Redirect("/Portal/Login");
            }
        }
    }
}