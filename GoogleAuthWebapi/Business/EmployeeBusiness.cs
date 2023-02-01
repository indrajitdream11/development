using GoogleAuthWebapi.DataConnection;
using GoogleAuthWebapi.Interface;
using GoogleAuthWebapi.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebApplication1.ViewModel;

namespace GoogleAuthWebapi.Business
{
    public class EmployeeBusiness:IEmployee
    {
        EmployeeDbModel EM ;
        DataConnections DB;
        public EmployeeBusiness()
        {
            this.EM = new EmployeeDbModel();
            this.DB = new DataConnections();
        }
        public List<EmployeeListViewModel> GetRecord(EmployeeRquestModel vm)
        {

            DataTable dt = EM.GetList(vm);
            return DB.ConvertDataTable<EmployeeListViewModel>(dt).ToList();
        }
    }
}