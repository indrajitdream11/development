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
        public ActionResult Edit(int id)
        {
            var ddl = _api.Post<IEnumerable<CountryDDLModel>, EmployeeAddUpdateViewModel>("Employee/DDL", new EmployeeAddUpdateViewModel()).Result;

            var obj = _api.Post<List<EmployeeListViewModel>, EmployeeRquestModel>("Employee/EmployeeList", new EmployeeRquestModel() { id=id}).Result;
            ViewBag.ddl = new SelectList(ddl, "cid", "name");
            return PartialView("_EmployeeAddEdit", obj[0]);
        }
        public JsonResult AddEdit(EmployeeAddUpdateViewModel vm)
        {
            var res = _api.Post<AddUpdateViewModel, EmployeeAddUpdateViewModel>("Employee/AddEdit", vm).Result;
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Add()
        {
            var ddl = _api.Post<IEnumerable<CountryDDLModel>, EmployeeAddUpdateViewModel>("Employee/DDL", new EmployeeAddUpdateViewModel()).Result;

            ViewBag.ddl = new SelectList(ddl, "cid", "name");
            return PartialView("_EmployeeAddEdit", new EmployeeListViewModel());
        }
        public ActionResult delete(int id)
        {
            var res = _api.Post<AddUpdateViewModel, EmployeeAddUpdateViewModel>("Employee/delete", new EmployeeAddUpdateViewModel { ID=id}).Result;
            return RedirectToAction("EmployeeList");
        }
    }
}