using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;


namespace GoogleAuthWebapi.DataConnection
{

    public class PArray
    {
        public String PName { get; set; }
        public Object PValue { get; set; }
        public SqlDbType PType { get; set; }
    }
    public class DataConnections
    {
 /// -----------------------------------------------------------------------------
            /// <title>makePArray</title>
            /// <summary>Function create an arraylist of SQL parameters.</summary>
            /// <param name="Parameter">The name of the parameter.</param>
            /// <param name="PValue">The value of the paramter.</param>
            /// <param name="SQLType">The SQL Data Input Type.</param>
            /// <example>
            /// <code>
            /// Dim DB as new ClassLibrary.Library.DBConn
            /// Dim ID as integer
            /// Dim myArray As New ArrayList
            /// myArray.add(DB.makePArray("@i_intID", ID, SqlDbType.Int))
            /// </code>
            /// </example>
            /// <returns>Arraylist</returns>
            /// <history>
            /// 	[Eddowes]	12/05/2011	Created
            /// </history>
            /// -----------------------------------------------------------------------------

            public PArray makePArray(String Parameter, Object PValue, SqlDbType SQLType)
            {
                PArray parameters = new PArray();
                parameters.PName = Parameter;
                parameters.PValue = PValue;
                parameters.PType = SQLType;
                return parameters;
            }

            /// -----------------------------------------------------------------------------
            /// <title>getData</title>
            /// <summary>Function returning to return a SQL dataset.</summary>
            /// <param name="SPName">The Stored Procedure name.</param>
            /// <param name="myPArray">An arraylist of input parameters and values.</param>
            /// <param name="myConn">The SQL database connection string.</param>
            /// <returns>Dataset</returns>
            /// <history>
            /// 	[Eddowes]	12/05/2011	Created
            /// </history>
            /// -----------------------------------------------------------------------------

            public DataSet getData(String SPName, List<PArray> myPArray, String myConn)
            {

                SqlConnection myConnection = new SqlConnection(myConn);

                // Create the command object, passing the SQL string.
                SqlDataAdapter myCommand = new SqlDataAdapter(SPName, myConnection);
                myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;
                myCommand.SelectCommand.CommandTimeout = 120;
                DataSet DS = new DataSet();

                // Loop the parameter list if not equal to nothing
                // to return individual parameter lists.

                if (myPArray != null)
                {
                    foreach (PArray parameter in myPArray)
                    {
                        using (var cmd = myCommand.SelectCommand)
                        {
                            cmd.Parameters.Add(new SqlParameter(parameter.PName, parameter.PType));
                            cmd.Parameters[parameter.PName].Value = RenderNull(parameter.PValue, parameter.PType);
                        }
                    }
                }

                // Test to see if the database is accessible and output as a Dataset.
                try
                {
                    myConnection.Open();
                    myCommand.Fill(DS, SPName);
                }




                catch (Exception ex)
                {
                }
                finally
                {
                    // Cleanup command and connection objects.
                    myPArray = null;
                    myConnection.Close();
                    myCommand.Dispose();
                    myConnection.Dispose();
                }

                return DS;

            }

            /// -----------------------------------------------------------------------------
            /// <title>GetDataTable</title>
            /// <summary>Function returning to return a SQL dataset.</summary>
            /// <param name="SPName">The Stored Procedure name.</param>
            /// <param name="myPArray">An arraylist of input parameters and values.</param>
            /// <param name="TableID">The index of the table to retrieve data from.</param>
            /// <param name="myConn">The SQL database connection string.</param>
            /// <returns>DataTable</returns>
            /// <history>
            /// 	[Eddowes]	12/05/2011	Created
            /// </history>
            /// -----------------------------------------------------------------------------

            public DataTable GetDataTable(String SPName, List<PArray> myPArray, Int32 TableID, String Conn)
            {
                DataSet DS = new DataSet();

                try
                {
                    DS = getData(SPName, myPArray, Conn);
                    DataTable dt = DS.Tables[TableID];
                    return dt;
                }
                catch { return null; }
                finally { DS.Dispose(); }
            }

            /// <title>GetSQLDBType</title>
            /// <summary>Function to return the correct SQLDBType for the given input.</summary>
            /// <param name="Obj">The Stored Procedure Parameter Name.</param>
            /// <returns>SqlDbType</returns>
            /// <history>
            /// 	[Eddowes]	14/03/2013	Created
            /// </history>
            /// -----------------------------------------------------------------------------

            public SqlDbType GetSQLDBType(String ParameterName)
            {
                SqlDbType SQLType = SqlDbType.VarChar;
                if (ParameterName.Contains("@i_bint"))
                    SQLType = SqlDbType.BigInt;
                else if (ParameterName.Contains("@i_bin"))
                    SQLType = SqlDbType.VarBinary;
                else if (ParameterName.Contains("@i_bit"))
                    SQLType = SqlDbType.Bit;
                else if (ParameterName.Contains("@i_dec"))
                    SQLType = SqlDbType.Decimal;
                else if (ParameterName.Contains("@i_dte"))
                    SQLType = SqlDbType.DateTime;
                else if (ParameterName.Contains("@i_int"))
                    SQLType = SqlDbType.Int;
                else if (ParameterName.Contains("@i_mny"))
                    SQLType = SqlDbType.Money;


                return SQLType;
            }

            /// <title>GetSQLDBType</title>
            /// <summary>Function to return the correct SQLDBType for the given input.</summary>
            /// <param name="Obj">The Stored Procedure Parameter Name.</param>
            /// <returns>SqlDbType</returns>
            /// <history>
            /// 	[Eddowes]	14/03/2013	Created
            /// </history>
            /// -----------------------------------------------------------------------------

            public List<PArray> Add(String ParameterName, Object Value)
            {
                PArray newPArray = new PArray();
                newPArray.PName = ParameterName;
                newPArray.PValue = Value;
                newPArray.PType = GetSQLDBType(ParameterName);
                PList.Add(newPArray);
                return PList;
            }

            public List<PArray> PList { get; set; }


            /// <title>RenderNull</title>
            /// <summary>Function render nulls to the database.</summary>
            /// <param name="Obj">The Stored Procedure object.</param>
            /// <returns>Object</returns>
            /// <history>
            /// 	[Eddowes]	12/05/2011	Created
            /// </history>
            /// -----------------------------------------------------------------------------

            public Object RenderNull(Object Obj, SqlDbType SQLType)
            {
                if (SQLType == SqlDbType.DateTime)
                {
                    if (Convert.ToDateTime(Obj) == DateTime.MinValue)
                        Obj = null;
                }

                if (Convert.IsDBNull(Obj) || Obj == null)
                    return System.DBNull.Value;
                else
                    return Obj;
            }

            /// <title>ConvertNull</title>
            /// <summary>Function to convert nulls for specific input types.</summary>
            /// <param name="input">The input value.</param>
            /// <returns>Object</returns>
            /// <history>
            /// 	[Eddowes]	14/03/2013	Created
            /// </history>
            /// -----------------------------------------------------------------------------
            public Object ConvertNull(Object input)
            {
                if (Convert.IsDBNull(input) == true)
                {
                    if (input.GetType() == typeof(Boolean))
                        return false;
                    else if (input.GetType() == typeof(Int32))
                        return Int32.MinValue;
                    else if (input.GetType() == typeof(Double))
                        return Double.MinValue;
                    else if (input.GetType() == typeof(DateTime))
                        return DateTime.MinValue;
                    else
                        return null;
                }
                else
                    return input;
            }

            /// -----------------------------------------------------------------------------
            /// <title>ConvertDataRowToModel</title>
            /// <summary>Function to render DataRow to a new instance of a Model.</summary>
            /// <param name="row">The input DataRow.</param>
            /// <param name="T">The input Model type.</param>
            /// <returns>The filled input Model.</returns>
            /// <history>
            /// 	[Eddowes]	22/03/2017	Created
            /// </history>
            /// -----------------------------------------------------------------------------
            public T ConvertDataRowToModel<T>(DataRow row) where T : new()
            {
                // create a new object
                T item = new T();

                // set the item
                SetItemFromRow(item, row);

                // return 
                return item;
            }

            /// -----------------------------------------------------------------------------
            /// <title>SetItemFromRow</title>
            /// <summary>Function to convert DataRow columns to the properties in a Model.</summary>
            /// <param name="item">A new instance of the input Model type (T).</param>
            /// <param name="row">The input DataRow.</param>
            /// <returns>Void - renders DataRow columns to the new instance of the input Model.</returns>
            /// <history>
            /// 	[Eddowes]	22/03/2017	Created
            /// </history>
            /// -----------------------------------------------------------------------------
            public void SetItemFromRow<T>(T item, DataRow row) where T : new()
            {
                // go through each column
                foreach (DataColumn c in row.Table.Columns)
                {
                    // find the property for the column
                    PropertyInfo p = item.GetType().GetProperty(c.ColumnName);

                    // if exists, set the value
                    if (p != null && row[c] != DBNull.Value)
                    {
                        p.SetValue(item, row[c], null);
                    }
                }
            }

            /// -----------------------------------------------------------------------------
            /// <title>ConvertDataTable</title>
            /// <summary>Function to convert a DataTable to enumerables of a Model (T).</summary>
            /// <param name="datatable">The input DataTable value.</param>
            /// <param name="T">The Model type to convert to.</param>
            /// <returns>Enumerables of the input Model (T).</returns>
            /// <history>
            /// 	[Eddowes]	23/03/2017	Created
            /// </history>
            /// -----------------------------------------------------------------------------
            public IEnumerable<T> ConvertDataTable<T>(DataTable datatable) where T : new()
            {
                if (datatable != null)
                {
                    foreach (DataRow row in datatable.Rows)
                        yield return ConvertDataRowToModel<T>(row);
                }
                else
                    yield break;
            }

            /// -----------------------------------------------------------------------------
            /// <title>ConvertDataRow</title>
            /// <summary>Function to convert a DataRow to a single instance of a Model.</summary>
            /// <param name="row">The input DataRow.</param>
            /// <param name="T">The Model type to convert to.</param>
            /// <returns>A single instance of the input Model (T).</returns>
            /// <history>
            /// 	[Eddowes]	23/03/2017	Created
            /// </history>
            /// -----------------------------------------------------------------------------
            public T ConvertDataRow<T>(DataRow row) where T : new()
            {
                if (row != null)
                    return ConvertDataRowToModel<T>(row);
                else
                    return default(T);

            }


            public DataRow GetReturnError(String errormessage)
            {
                // Here we create a DataTable with four columns.
                DataTable table = new DataTable();
                table.Columns.Add("ReturnID", typeof(Int32));
                table.Columns.Add("Message", typeof(String));
                table.Columns.Add("IsSuccessful", typeof(Boolean));

                // Here we add five DataRows.
                table.Rows.Add(null, errormessage, false);

                return table.Rows[0];
            }
        }
    }
