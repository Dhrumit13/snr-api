using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SNR_Data
{
    public interface ISQLHelper
    {
        bool GetDBConnectionStatus(bool prevConnState);
        DataSet GetDataTableByID(string storedProcedureName, string sqlParameterName, int sqlParmeterValue);
        long BeginTrans();
        int InsertObjectLite(object objBo, string tableName);
        object ExecuteNonQuery(SqlParameter[] paras, string cmdText);
        void CommitTrans(long transID);
        void RollBackTrans(long transID);
        int UpdateObjectLite(object objBo, string tableName);
        int DeleteObjectLite(int primaryKeyVal, int modifiedBy, string tableName);
        DataSet ReturnWithDataSet(SqlParameter para, string cmdText);
        DataSet ReturnWithDataSet(SqlParameter[] paras, string cmdText);
        DataTable ReturnWithDataTable(SqlParameter[] paras, string cmdText);
        int Return_CUD_flag(SqlParameter[] parameters, string procedure);
        int ExecuteNonQuery(string query, ref bool executionSucceeded);
        object ExecuteScalar(string query);
        SqlConnection CloseConnection();
        SqlCommand CreateCommandObject(SqlParameter[] paras, string cmdText);
        SqlCommand CreateCommandObject(SqlParameter para, string cmdText);
        string GetProcedureName(string table, Enums.OperationType operation);
        DataSet return_DataSet(string str_SqlCommand);
        DataTable return_DataTable(string str_SqlCommand);
        object ReturnScalarValue(SqlParameter[] parameters, string cmdText);
    }

    public class SQLHelper : ISQLHelper
    {
        public readonly string _dbConnString;

        public SqlConnection _sqlConnection;
        public bool _isInTransaction;
        public SqlTransaction _sqlTransaction;
        public long _sqlTransactionID;
        private DateTime _startTime;
        private DateTime _endTime;
        private SqlCommand _sqlCommand;

        public SQLHelper(string dbConnString)
        {
            _dbConnString = dbConnString;
        }

        public bool GetDBConnectionStatus(bool prevConnState)
        {
            if (string.IsNullOrEmpty(_dbConnString))
            {
                return false;
            }
            using (SqlConnection conn = new SqlConnection(_dbConnString))
            {
                try
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public long BeginTrans()
        {
            long l_lng_BeginTrans = 0;

            _sqlConnection = new SqlConnection(_dbConnString);

            try
            {
                if (_sqlConnection != null && (_sqlConnection.ConnectionString == "" || string.IsNullOrEmpty(_sqlConnection.ConnectionString)))
                {
                    _sqlConnection.ConnectionString = _dbConnString;
                }
                if (_isInTransaction == false)
                {
                    //Get Start time of Transaction
                    _startTime = DateTime.Now;

                    _sqlTransactionID = _sqlTransactionID + 1;
                    _isInTransaction = true;

                    _sqlConnection.Open();
                    _sqlTransaction = _sqlConnection.BeginTransaction();

                    l_lng_BeginTrans = _sqlTransactionID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return l_lng_BeginTrans;
        }

        public void CommitTrans(long transID)
        {
            try
            {
                if (_isInTransaction == true)
                {
                    if (_sqlTransactionID == transID)
                    {
                        _sqlTransaction.Commit();
                        _isInTransaction = false;

                        _sqlConnection.Close();
                        //Get End Time When Main Transaction is Done
                        _endTime = DateTime.Now;
                        //Get end time
                        //get transaction time in miliseconds
                        //reset start time and end time
                        _startTime = DateTime.Now;
                        _endTime = DateTime.Now;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RollBackTrans(long transID)
        {
            try
            {
                if (_isInTransaction == true)
                {
                    if (_sqlTransactionID == transID)
                    {
                        _sqlTransaction.Rollback();
                        _isInTransaction = false;
                        _sqlConnection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertObjectLite(object objBo, string tableName)
        {
            Type type = default(Type);
            PropertyInfo[] propertiesInfo = null;
            string procedureName = GetProcedureName(tableName, Enums.OperationType.Insert);

            type = objBo.GetType();
            int paraLength = propertiesInfo.Length;
            propertiesInfo = type.GetProperties();
            if (propertiesInfo[propertiesInfo.Length - 1].PropertyType == typeof(Object))
            {
                paraLength = propertiesInfo.Length - 1;
            }

            SqlParameter[] parameters = new SqlParameter[paraLength];
            try
            {
                for (var i = 0; i <= propertiesInfo.Length - 1; i++)
                {
                    var info = propertiesInfo[i];
                    var para = new SqlParameter { ParameterName = string.Format("@{0}", info.Name) };

                    if (type.GetProperty(propertiesInfo[i].Name).PropertyType.ToString() == "System.DateTime" && type.GetProperty(info.Name).GetValue(objBo, null).ToString() == "1/1/0001 12:00:00 AM")
                    {
                        para.Value = type.GetProperty(info.Name).GetValue(objBo, null);
                        para.Value = DBNull.Value;
                    }
                    else
                    {
                        para.Value = type.GetProperty(info.Name).GetValue(objBo, null);
                    }
                    parameters[i] = para;
                }

                parameters[0].Direction = ParameterDirection.Output;
                var result = Convert.ToInt32(ExecuteNonQuery(parameters, procedureName));
                return Convert.ToInt32(parameters[0].SqlValue.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateObjectLite(object objBo, string tableName)
        {
            Type type = default(Type);
            PropertyInfo[] propertiesInfo = null;
            string procedureName = GetProcedureName(tableName, Enums.OperationType.Update);
            int i = 0;
            var para = default(SqlParameter);

            type = objBo.GetType();
            propertiesInfo = type.GetProperties();
            var parameters = new SqlParameter[propertiesInfo.Length + 1];

            try
            {
                for (i = 0; i <= propertiesInfo.Length - 1; i++)
                {
                    var info = propertiesInfo[i];
                    para = new SqlParameter { ParameterName = string.Format("@{0}", info.Name) };

                    if (type.GetProperty(propertiesInfo[i].Name).PropertyType.ToString() == "System.DateTime" && propertiesInfo[i].Name == "CreatedDate")
                    {
                        para.Value = type.GetProperty(info.Name).GetValue(objBo, null);
                        var testVal = para.Value;
                        if (testVal.ToString().Contains("12:00:00 AM"))
                        {
                            para.Value = DBNull.Value;
                        }
                    }
                    else
                    {
                        para.Value = type.GetProperty(info.Name).GetValue(objBo, null);
                    }
                    parameters[i] = para;
                }
                para = new SqlParameter
                {
                    ParameterName = "@Flag",
                    Value = 0,
                    Direction = ParameterDirection.Output
                };
                parameters[i] = para;
                i = Convert.ToInt32(ExecuteNonQuery(parameters, procedureName));
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeleteObjectLite(int primaryKeyVal, int modifiedBy, string tableName)
        {
            int i = 0;
            SqlParameter para;
            string procedureName = "";
            DataSet dsPrimaryKey = new DataSet();
            SqlParameter param = new SqlParameter();
            SqlParameter[] parameters = new SqlParameter[3];
            try
            {
                procedureName = GetProcedureName(tableName, Enums.OperationType.Delete);
                param = new SqlParameter();
                param.ParameterName = "@table_name";
                param.Value = tableName;

                dsPrimaryKey = ReturnWithDataSet(param, "sp_pkeys");
                string pkName = "";
                if (dsPrimaryKey.Tables[0].Rows.Count > 0)
                {
                    pkName = dsPrimaryKey.Tables[0].Rows[0]["COLUMN_NAME"].ToString();
                }

                para = new SqlParameter
                {
                    ParameterName = string.Format("@a_lng_{0}", pkName),
                    Value = primaryKeyVal,
                    Direction = ParameterDirection.Input
                };
                parameters[0] = para;

                para = new SqlParameter
                {
                    ParameterName = "@a_lng_ModifiedBy",
                    Value = modifiedBy,
                    Direction = ParameterDirection.Input
                };
                parameters[1] = para;

                para = new SqlParameter
                {
                    ParameterName = "@Flag",
                    Value = 0,
                    Direction = ParameterDirection.Output
                };
                parameters[2] = para;

                i = Convert.ToInt32(ExecuteNonQuery(parameters, procedureName));
                return Convert.ToInt32(parameters[2].SqlValue.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetDataTableByID(string storedProcedureName, string sqlParameterName, int sqlParmeterValue)
        {

            try
            {
                using (_sqlConnection = new SqlConnection())
                {
                    _sqlConnection.ConnectionString = _dbConnString;
                    _sqlConnection.Open();
                    DataTable tableTemp = new DataTable();
                    DataSet ds = new DataSet();

                    using (_sqlCommand = new SqlCommand())
                    {
                        _sqlCommand.Connection = _sqlConnection;
                        _sqlCommand.CommandText = storedProcedureName;
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.Parameters.Add(
                            new SqlParameter(string.Format("@{0}", sqlParameterName), SqlDbType.Int)
                            {
                                Value = sqlParmeterValue
                            });
                        SqlDataAdapter myAdapter = new SqlDataAdapter(_sqlCommand);
                        myAdapter.Fill(ds);

                        return ds;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _sqlCommand.Connection = CloseConnection();
            }
        }

        public DataSet ReturnWithDataSet(SqlParameter para, string cmdText)
        {
            DataSet l_ds_Data = new DataSet();
            SqlCommand l_sql_Cmd = new SqlCommand();
            SqlDataAdapter l_sql_da = new SqlDataAdapter();
            try
            {
                l_sql_Cmd = CreateCommandObject(para, cmdText);
                l_sql_da.SelectCommand = l_sql_Cmd;
                l_sql_da.Fill(l_ds_Data);
            }
            catch (Exception ex)
            {
                l_ds_Data = null;
                throw ex;
            }
            finally
            {
                if (_isInTransaction == false)
                {
                    _sqlConnection.Close();
                }
            }
            return (l_ds_Data);
        }

        public DataSet ReturnWithDataSet(SqlParameter[] paras, string cmdText)
        {
            if (_dbConnString == "")
            {

                _sqlConnection = new SqlConnection(_dbConnString);
            }
            DataSet l_ds_Data = new DataSet();
            SqlCommand l_sql_Cmd = new SqlCommand();
            SqlDataAdapter l_sql_da = new SqlDataAdapter();
            try
            {
                l_sql_Cmd = CreateCommandObject(paras, cmdText);
                l_sql_da.SelectCommand = l_sql_Cmd;
                l_sql_da.Fill(l_ds_Data);
            }
            catch (Exception ex)
            {
                l_ds_Data = null;
                throw ex;
            }
            finally
            {
                if (_isInTransaction == false)
                {
                    _sqlConnection.Close();
                }
            }
            return (l_ds_Data);
        }

        public DataSet return_DataSet(string str_SqlCommand)
        {
            try
            {
                using (_sqlConnection = new SqlConnection())
                {
                    _sqlConnection.ConnectionString = _dbConnString;
                    _sqlConnection.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        using (_sqlCommand = new SqlCommand())
                        {
                            _sqlCommand.Connection = _sqlConnection;
                            _sqlCommand.CommandText = str_SqlCommand;
                            _sqlCommand.CommandType = CommandType.Text;
                            da.SelectCommand = _sqlCommand;

                            using (DataSet ds = new DataSet())
                            {
                                da.Fill(ds, "table1");
                                return ds;
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                _sqlCommand.Connection = CloseConnection();
            }
        }

        public DataTable return_DataTable(string str_SqlCommand)
        {
            try
            {
                using (_sqlConnection = new SqlConnection())
                {
                    _sqlConnection.ConnectionString = _dbConnString;
                    _sqlConnection.Open();
                    SqlDataReader reader_Temp;
                    using (_sqlCommand = new SqlCommand())
                    {
                        DataTable Table_Temp = new DataTable();
                        _sqlCommand.Connection = _sqlConnection;
                        _sqlCommand.CommandText = str_SqlCommand;
                        _sqlCommand.CommandType = CommandType.Text;
                        reader_Temp = _sqlCommand.ExecuteReader();
                        if (reader_Temp.HasRows == true)
                        {
                            Table_Temp.Load(reader_Temp);
                            reader_Temp.Close();
                            return Table_Temp;
                        }
                        else
                        {
                            reader_Temp.Close();
                            return Table_Temp;
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                _sqlCommand.Connection = CloseConnection();
            }
        }

        public DataTable ReturnWithDataTable(SqlParameter[] paras, string cmdText)
        {
            if (_dbConnString == "")
            {

                _sqlConnection = new SqlConnection(_dbConnString);
            }
            DataTable l_ds_Data = new DataTable();
            SqlCommand l_sql_Cmd = new SqlCommand();
            SqlDataAdapter l_sql_da = new SqlDataAdapter();
            try
            {
                l_sql_Cmd = CreateCommandObject(paras, cmdText);
                l_sql_da.SelectCommand = l_sql_Cmd;
                l_sql_da.Fill(l_ds_Data);
            }
            catch (Exception ex)
            {
                l_ds_Data = null;
                throw ex;
            }
            finally
            {
                if (_isInTransaction == false)
                {
                    _sqlConnection.Close();
                }
            }
            return (l_ds_Data);
        }

        public int Return_CUD_flag(SqlParameter[] parameters, string procedure)
        {
            SqlCommand cmd = new SqlCommand();
            cmd = CreateCommandObject(parameters, procedure);
            int returnFlag = cmd.ExecuteNonQuery();
            return returnFlag;
        }

        public int ExecuteNonQuery(string query, ref bool executionSucceeded)
        {
            int i = -1;

            try
            {
                using (_sqlConnection = new SqlConnection())
                {
                    _sqlConnection.ConnectionString = _dbConnString;
                    _sqlConnection.Open();

                    SqlCommand objCommand = new SqlCommand
                    {
                        CommandType = CommandType.Text,
                        CommandText = query,
                        Connection = _sqlConnection
                    };
                    i = objCommand.ExecuteNonQuery();
                    executionSucceeded = true;
                }
            }
            catch (Exception ex)
            {
                executionSucceeded = false;
                throw ex;
            }
            return i;
        }

        public object ExecuteScalar(string query)
        {

            try
            {
                using (_sqlConnection = new SqlConnection())
                {
                    _sqlConnection.ConnectionString = _dbConnString;
                    _sqlConnection.Open();

                    SqlCommand objCommand = new SqlCommand
                    {
                        CommandType = CommandType.Text,
                        CommandText = query,
                        Connection = _sqlConnection
                    };
                    var retval = objCommand.ExecuteScalar();
                    return retval;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object ReturnScalarValue(SqlParameter[] parameters, string cmdText)
        {
            try
            {
                using (_sqlConnection = new SqlConnection())
                {
                    _sqlConnection.ConnectionString = _dbConnString;
                    _sqlConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdText, _sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddRange(parameters);
                        return cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object ExecuteNonQuery(SqlParameter[] paras, string cmdText)
        {

            _sqlConnection = new SqlConnection(_dbConnString);
            int ReturnID = 0;
            SqlCommand l_sql_Cmd = new SqlCommand();

            try
            {
                l_sql_Cmd = CreateCommandObject(paras, cmdText);
                ReturnID = l_sql_Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_isInTransaction == false)
                {
                    _sqlConnection.Close();
                }
            }

            return (ReturnID);
        }

        public SqlConnection CloseConnection()
        {
            if (_sqlConnection.State == ConnectionState.Open)
            {
                _sqlConnection.Close();
            }
            return _sqlConnection;
        }

        public SqlCommand CreateCommandObject(SqlParameter[] paras, string cmdText)
        {

            _sqlConnection = new SqlConnection(_dbConnString);
            if (_sqlConnection != null && (_sqlConnection.ConnectionString == "" || string.IsNullOrEmpty(_sqlConnection.ConnectionString)))
            {
                _sqlConnection.ConnectionString = _dbConnString;
            }

            SqlCommand l_sql_Cmd = new SqlCommand();
            try
            {
                l_sql_Cmd.CommandType = CommandType.StoredProcedure;
                l_sql_Cmd.CommandText = cmdText;

                if (_sqlConnection.State == ConnectionState.Closed)
                {
                    _sqlConnection.Open();
                }

                l_sql_Cmd.Connection = _sqlConnection;

                ////if (m_bln_IsInTransaction == true)
                ////{
                ////    l_sql_Cmd.Transaction = m_trn_Transaction;
                ////}

                if (paras == null)
                {
                    return (l_sql_Cmd);
                }

                foreach (SqlParameter para in paras)
                {
                    l_sql_Cmd.Parameters.Add(para);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (l_sql_Cmd);
        }

        public SqlCommand CreateCommandObject(SqlParameter para, string cmdText)
        {

            _sqlConnection = new SqlConnection(_dbConnString);
            if (_sqlConnection != null && (_sqlConnection.ConnectionString == "" || string.IsNullOrEmpty(_sqlConnection.ConnectionString)))
            {
                _sqlConnection.ConnectionString = _dbConnString;
            }
            SqlCommand l_sql_Cmd = new SqlCommand();
            try
            {
                l_sql_Cmd.CommandType = CommandType.StoredProcedure;
                l_sql_Cmd.CommandText = cmdText;

                if (_sqlConnection.State == ConnectionState.Closed)
                {
                    _sqlConnection.Open();
                }

                l_sql_Cmd.Connection = _sqlConnection;

                //if (m_bln_IsInTransaction == true)
                //{
                //    l_sql_Cmd.Transaction = m_trn_Transaction;
                //}

                if (para == null)
                {
                    return (l_sql_Cmd);
                }

                l_sql_Cmd.Parameters.Add(para);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (l_sql_Cmd);
        }

        public string GetProcedureName(string table, Enums.OperationType operation)
        {
            var procedureName = "proc" + "_" + table + "_" + operation;
            return procedureName;
        }


    }

    public class Enums
    {
        public enum DefaultText
        {
            [Description("--Select--")]
            DropDownDefaultText = 0,

            [Description("Please select any instance")]
            SelectInstance,
        }
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
        public enum OperationType
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            GetAll = 4
        }
    }
}
