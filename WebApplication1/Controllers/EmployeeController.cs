using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.ApiConume;
using WebApplication1.ViewModel;

namespace WebApplication1.Controllers
{
    public class EmployeeController : Controller
    {
         ApiModel _api;
        public EmployeeController()
        {
            _api = new ApiModel();
                
        }
        // GET: Employee
        [HttpGet]
        public ActionResult EmployeeList()
        {
            List<EmployeeListViewModel> list = new List<EmployeeListViewModel>();
            var obj = _api.Post<List<EmployeeListViewModel>, EmployeeRquestModel>("Employee/EmployeeList", new EmployeeRquestModel()).Result;
            return View(obj);
        }
    }
}