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

        //public DataTable GetDocumentByID(Int32? id)
        //{
        //    DataSet DS = new DataSet();
        //    try
        //    {
        //        DB.PList = new List<PArray>();
        //        DB.Add(DocumentTypeID, id);
        //        DB.Add(StoredProcedures.GlobalDefaultMaximumRows, Global.DefaultPageListSize());
        //        DS = DB.getData(StoredProcedures.usp_DocumentType_Get, DB.PList, Global.ConnectionString().ToString());

        //        if (DS.Tables[0].Rows.Count > 0)
        //            return DS.Tables[0];
        //        else
        //            return null;
        //    }
        //    catch { return null; }
        //    finally { DS.Dispose(); }
        //}

        //public DocumentTypeResponseViewModel AddDocumentType(DocumentTypeInsertViewModel csvm)
        //{
        //    Int32 ID = 0;
        //    String Message = null;
        //    Boolean Successful = false;
        //    DataSet DS = new DataSet();
        //    try
        //    {
        //        DB.PList = new List<PArray>();
        //        DB.Add(DocumentTypeID, csvm.DocumentTypeID);
        //        DB.Add(DocumentTypeName, csvm.DocumentTypeName);
        //        DB.Add(Description, csvm.Description);
        //        DS = DB.getData(StoredProcedures.usp_DocumentType_AddUpdate, DB.PList, Global.ConnectionString().ToString());

        //        DataRow row = DS.Tables[0].Rows[0];
        //        ID = row.Field<Int32>("ID");
        //        Message = row.Field<String>("Message");
        //        Successful = row.Field<Boolean>("Successful");
        //    }
        //    catch (Exception ex)
        //    {
        //        Message = "A database error has occured.";
        //    }
        //    finally { DS.Dispose(); }

        //    return new DocumentTypeResponseViewModel()
        //    {
        //        ID = ID,
        //        Message = Message,
        //        Successful = Successful
        //    };
        //}

        //public DocumentTypeResponseViewModel UpdateDocumentType(DocumentTypeUpdateViewModel csvm)
        //{
        //    Int32 ID = 0;
        //    String Message = null;
        //    Boolean Successful = false;
        //    DataSet DS = new DataSet();
        //    try
        //    {
        //        DB.PList = new List<PArray>();
        //        DB.Add(DocumentTypeID, csvm.DocumentTypeID);
        //        DB.Add(DocumentTypeName, csvm.DocumentTypeName);
        //        DB.Add(Description, csvm.Description);
        //        DS = DB.getData(StoredProcedures.usp_DocumentType_AddUpdate, DB.PList, Global.ConnectionString().ToString());

        //        DataRow row = DS.Tables[0].Rows[0];
        //        ID = row.Field<Int32>("ID");
        //        Message = row.Field<String>("Message");
        //        Successful = row.Field<Boolean>("Successful");
        //    }
        //    catch (Exception ex)
        //    {
        //        Message = "A database error has occured.";
        //    }
        //    finally { DS.Dispose(); }

        //    return new DocumentTypeResponseViewModel()
        //    {
        //        ID = ID,
        //        Message = Message,
        //        Successful = Successful
        //    };
        //}


        //public DocumentTypeResponseViewModel DeleteDocumentType(DocumentTypeDeleteViewModel csvm)
        //{
        //    Int32 ID = 0;
        //    String Message = null;
        //    Boolean Successful = false;
        //    DataSet DS = new DataSet();
        //    try
        //    {
        //        DB.PList = new List<PArray>();
        //        DB.Add(DocumentTypeID, csvm.DocumentTypeID);
        //        DS = DB.getData(StoredProcedures.usp_DocumentType_Delete, DB.PList, Global.ConnectionString().ToString());

        //        DataRow row = DS.Tables[0].Rows[0];
        //        ID = row.Field<Int32>("ID");
        //        Message = row.Field<String>("Message");
        //        Successful = row.Field<Boolean>("Successful");
        //    }
        //    catch (Exception ex)
        //    {
        //        Message = "A database error has occured.";
        //    }
        //    finally { DS.Dispose(); }

        //    return new DocumentTypeResponseViewModel()
        //    {
        //        ID = ID,
        //        Message = Message,
        //        Successful = Successful
        //    };
        //}
    }
    #endregion
}
