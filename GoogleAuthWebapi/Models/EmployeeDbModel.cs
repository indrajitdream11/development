using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using GoogleAuthWebapi.DataConnection;
using WebApplication1.Config;
using WebApplication1.ViewModel;

namespace GoogleAuthWebapi.Models
{
    public class EmployeeDbModel
    {
        
        private DataConnections DB = new DataConnections();
        
        public EmployeeDbModel()
        {
          
            DB = new DataConnections();
        }

        // Constants.
        #region Constants
        private const String AddressID = "@i_intAddressID";
        private const String SearchText = "@i_strSearchText";
        private const String AddressTypeID = "@i_intAddressTypeID";


        //For Add/Update SP
        private const String DocumentTypeID = "@i_intDocumentTypeID";
        private const String DocumentTypeName = "@i_strDocumentTypeName";
        private const String Description = "@i_strDescription";

        #endregion

        #region Public Methods

        public DataTable GetList(EmployeeRquestModel vm)
        {
            DataSet DS = new DataSet();
            try
            {
                DB.PList = new List<PArray>();
                DB.Add("@i_intEmployee", vm.id);
               
                DS = DB.getData("usp_Employee_Get", DB.PList, Constant.dbcon);

                if (DS.Tables[0].Rows.Count > 0)
                    return DS.Tables[0];
                else
                    return null;
            }
            catch { return null; }
            finally { DS.Dispose(); }
        }

        public DataTable GetDDlList()
        {
            DataSet DS = new DataSet();
            try
            {
                DB.PList = new List<PArray>();                
                DS = DB.getData("usp_Country_Get", DB.PList, Constant.dbcon);

                if (DS.Tables[0].Rows.Count > 0)
                    return DS.Tables[0];
                else
                    return null;
            }
            catch { return null; }
            finally { DS.Dispose(); }
        }

        public AddUpdateViewModel AddEdit(EmployeeAddUpdateViewModel csvm)
        {
            Int32 ID = 0;
            String Message = null;
            Boolean Successful = false;
            DataSet DS = new DataSet();
            try
            {
                DB.PList = new List<PArray>();
                DB.Add("@i_intEmployee", csvm.ID);
                DB.Add("@i_strName", csvm.Name);
                DB.Add("@i_intCountryID", csvm.CountryID);
                DB.Add("@i_strEmail", csvm.Email);
                DS = DB.getData("usp_Employee_AddUpdate", DB.PList, Constant.dbcon);

                DataRow row = DS.Tables[0].Rows[0];
                ID = row.Field<Int32>("ID");
                Message = row.Field<String>("Message");
                Successful = row.Field<Boolean>("Successful");
            }
            catch (Exception ex)
            {
                Message = "A database error has occured.";
            }
            finally { DS.Dispose(); }

            return new AddUpdateViewModel()
            {
                ID = ID,
                Message = Message,
                Successful = Successful
            };
        }
        public AddUpdateViewModel Delete(EmployeeAddUpdateViewModel csvm)
        {
            Int32 ID = 0;
            String Message = null;
            Boolean Successful = false;
            DataSet DS = new DataSet();
            try
            {
                DB.PList = new List<PArray>();
                DB.Add("@i_intEmployee", csvm.ID);
               
                DS = DB.getData("usp_Employee_Delete", DB.PList, Constant.dbcon);

                DataRow row = DS.Tables[0].Rows[0];
                ID = row.Field<Int32>("ID");
                Message = row.Field<String>("Message");
                Successful = row.Field<Boolean>("Successful");
            }
            catch (Exception ex)
            {
                Message = "A database error has occured.";
            }
            finally { DS.Dispose(); }

            return new AddUpdateViewModel()
            {
                ID = ID,
                Message = Message,
                Successful = Successful
            };
        }
    }
    #endregion
}
