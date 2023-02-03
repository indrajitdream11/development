using GoogleAuthWebapi.Business;
using GoogleAuthWebapi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApplication1.ViewModel;

namespace GoogleAuthWebapi.Controllers
{
    [RoutePrefix("api/Employee")]
    public class EmployeeController : ApiController
    {
       // IEmployee _iEmployee;
        EmployeeBusiness _iEmployee = new EmployeeBusiness();
        /// <summary>
        /// Initialises a new instance of the interface.
        /// </summary>
        //public EmployeeController(IEmployee iEmployee)
        //{
        //    _iEmployee = iEmployee;
        //}
       
        [HttpPost]
        [Route("EmployeeList")]
        public async Task<IHttpActionResult> GetList([FromBody] EmployeeRquestModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_iEmployee.GetRecord(vm));
        }
        [HttpPost]
        [Route("DDL")]
        public async Task<IHttpActionResult> GetDDLList([FromBody] EmployeeAddUpdateViewModel vm)
        {
         
            return Ok(_iEmployee.GetDDLList());
        }
        [HttpPost]
        [Route("AddEdit")]
        public async Task<IHttpActionResult> AddEdit([FromBody] EmployeeAddUpdateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_iEmployee.AddEdit(vm));
        }
        [HttpPost]
        [Route("delete")]
        public async Task<IHttpActionResult> delete([FromBody] EmployeeAddUpdateViewModel vm)
        {
           
            return Ok(_iEmployee.Delete(vm));
        }
    }
}
