using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VendasMVC.Models.Util
{
    public class SessionTimeoutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            if (HttpContext.Current.Session["IdVendedor"] == null)
            {
                filterContext.Result = new RedirectResult("/login");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}