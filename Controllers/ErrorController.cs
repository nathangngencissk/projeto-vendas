using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VendasMVC.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/NotFound")]
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View();
        }
    }
}