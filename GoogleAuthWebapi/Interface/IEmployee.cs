using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.ViewModel;

namespace GoogleAuthWebapi.Interface
{
    public interface IEmployee
    {
        List<EmployeeListViewModel> GetRecord(EmployeeRquestModel vm);
        IEnumerable<CountryDDLModel> GetDDLList();
        AddUpdateViewModel AddEdit(EmployeeAddUpdateViewModel csvm);
        AddUpdateViewModel Delete(EmployeeAddUpdateViewModel csvm);
    }
}
