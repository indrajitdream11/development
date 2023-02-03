using GoogleAuthWebapi.CustomFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace GoogleAuthWebapi.Controllers
{
    public class HomeController : Controller
    {
      //  [LogFilter]
        public ActionResult Index()
        {
            
            return View();
        }
        
    }

}
