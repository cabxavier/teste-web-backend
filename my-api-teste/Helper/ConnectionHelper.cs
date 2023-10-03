using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using Enum;
using MyAttribute;
using Args;
using Interface;

namespace Helper
{
    public class ConnectionHelper
    {
        const string MSG_ERROR = "[MSVConnectionHelper] [{0}] [{1}]";

        private SqlConnection _Conn;
        private SqlTransaction _Tran;
        private bool _CloseConnectionAfterExecute = false;
        private static string _ConnectionString;

        static ConnectionHelper()
        {
            _ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
        }

        #region Método SetParameters
        private void SetParameters(SqlCommand oCmd, List<SqlParameter> aParam)
        {
            if (aParam != null)
            {
                aParam.ForEach(p => oCmd.Parameters.Add(p));
            }
        }
        #endregion


        #region Método IsConnectionOpen
        private bool IsConnectionOpen()
        {
            return ((_Conn != null) && (_Conn.State == ConnectionState.Open));
        }
        #endregion


        #region Método PrepareSqlCommand
        private SqlCommand PrepareSqlCommand(string Stmt, CommandType CmdType, List<SqlParameter> Params = null)
        {
            SqlCommand _Cmd = null;


            if (!this.IsConnectionOpen())
            {
                this.Open();

                this._CloseConnectionAfterExecute = true;
            }

            _Cmd = new SqlCommand(Stmt, _Conn);
            _Cmd.CommandType = CmdType;

            if (_Tran != null)
            {
                _Cmd.Transaction = _Tran;
            }

            if ((Params != null) && (Params.Count > 0))
            {
                this.SetParameters(_Cmd, Params);
            }

            return _Cmd;
        }
        #endregion


        #region Método ToSqlParameter
        private List<SqlParameter> ToSqlParameter(IDto Dto, PostAction PostAction, bool OperationLog = false)
        {
            List<SqlParameter> _Out = new List<SqlParameter>();

            try
            {
                string _ParameterName = null;
                string _ParameterType = null;
                object _ParameterValue = null;

                PostAttribute _PostAttibute = null;
                PrimaryKeyAttribute _PrimaryKeyAttribute = null;
                IntegerAttribute _IntegerAttribute = null;

                List<PropertyInfo> lstInfos = Dto.GetType().GetProperties().ToList();

                foreach (PropertyInfo _PropertyInfo in lstInfos)
                {
                    _PostAttibute = (PostAttribute)_PropertyInfo.GetCustomAttribute(typeof(PostAttribute));

                    _IntegerAttribute = (IntegerAttribute)_PropertyInfo.GetCustomAttribute(typeof(IntegerAttribute));

                    _PrimaryKeyAttribute = (PrimaryKeyAttribute)_PropertyInfo.GetCustomAttribute(typeof(PrimaryKeyAttribute));

                    if (_PostAttibute != null)
                    {                        
                        _ParameterName = _PropertyInfo.Name;

                        _ParameterValue = _PropertyInfo.GetValue(Dto, null);
                        
                        _ParameterType = _PropertyInfo.PropertyType.Name;

                        switch (PostAction)
                        {
                            case PostAction.Insert:

                                if (_PostAttibute.PostInsert)
                                {
                                    if (_ParameterType.ToLower() == "int32")
                                    {
                                        if (_PrimaryKeyAttribute != null)
                                        {
                                            _ParameterValue = 0;
                                        }
                                        else
                                        {
                                            if (Convert.ToInt32(_ParameterValue) == 0)
                                            {
                                                if (_IntegerAttribute != null && _IntegerAttribute.Required == false)
                                                {
                                                    _ParameterValue = DBNull.Value;
                                                }
                                            }
                                        }
                                    }

                                    if (_ParameterType.ToLower() == "datetime")
                                    {
                                        if (Convert.ToDateTime(_ParameterValue) == DateTime.MinValue)
                                        {
                                            _ParameterValue = DBNull.Value;
                                        }
                                    }

                                    _Out.Add(new SqlParameter(_ParameterName, _ParameterValue));

                                    if (_PrimaryKeyAttribute != null)
                                    {
                                        _Out.Last().Direction = ParameterDirection.Output;
                                    }
                                }
                                break;

                            case PostAction.Update:

                                if (_PostAttibute.PostUpdate)
                                {
                                    if (_ParameterType == "DateTime")
                                    {
                                        if (Convert.ToDateTime(_ParameterValue) == DateTime.MinValue)
                                        {
                                            _ParameterValue = DBNull.Value;
                                        }
                                    }

                                    _Out.Add(new SqlParameter(_ParameterName, _ParameterValue));
                                }
                                break;

                            case PostAction.Delete:

                                if (_PostAttibute.PostDelete)
                                {
                                    _Out.Add(new SqlParameter(_ParameterName, _ParameterValue));
                                }
                                break;
                        }
                    }
                }

                _Out.Add(new SqlParameter("GerarLog", OperationLog));
            }
            catch
            {
                throw;
            }

            return _Out;
        }
        #endregion


        #region Método Open
        public void Open(bool WithTransaction = false)
        {
            _Conn = new SqlConnection(_ConnectionString);

            _Conn.Open();

            _CloseConnectionAfterExecute = false;

            if (WithTransaction)
            {
                _Tran = _Conn.BeginTransaction();
            }
        }
        #endregion


        #region Método Close
        public void Close(bool Sucess = true)
        {
            if (_Tran != null)
            {
                if (Sucess)
                {
                    _Tran.Commit();
                }
                else
                {
                    _Tran.Rollback();
                }
            }

            if (_Conn != null)
            {
                if (_Conn.State == ConnectionState.Open)
                {
                    _Conn.Close();
                }
            }
        }
        #endregion


        #region Método IsOpened
        public bool IsOpened()
        {
            return _Conn != null && _Conn.State != ConnectionState.Broken && _Conn.State != ConnectionState.Closed;
        }
        #endregion


        #region Método ExecuteScalar
        public object ExecuteScalar(string Stmt, CommandType CmdType, List<SqlParameter> Params = null)
        {
            object _Out = null;

            try
            {
                SqlCommand _Cmd = this.PrepareSqlCommand(Stmt, CmdType, Params);
                _Out = _Cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                this.Close(false);
                
                throw new Exception(string.Format(MSG_ERROR, "ExecuteScalar", e.Message));
            }
            finally
            {
                if (this._CloseConnectionAfterExecute)
                {
                    this.Close(true);
                }
            }

            return _Out;
        }
        #endregion


        #region Método ExecuteDataTable
        public DataTable ExecuteDataTable(string Stmt, CommandType CmdType, List<SqlParameter> Params = null)
        {
            DataTable _Out = new DataTable();

            SqlDataReader _Dr = null;

            try
            {
                SqlCommand _Cmd = this.PrepareSqlCommand(Stmt, CmdType, Params);
                _Dr = _Cmd.ExecuteReader();

                if (_Dr.HasRows)
                {
                    _Out = new DataTable();
                    _Out.Load(_Dr);
                }

            }
            catch (Exception e)
            {
                this.Close(false);

                throw new Exception(string.Format(MSG_ERROR, "ExecuteDataTable", e.Message));
            }
            finally
            {
                if ((_Dr != null) && (_Dr.IsClosed == false))
                {
                    _Dr.Close();
                }

                if (this._CloseConnectionAfterExecute)
                {
                    this.Close(true);
                }
            }

            return _Out;
        }
        #endregion


        #region Método ExecuteNonQuery
        public int ExecuteNonQuery(string Stmt, CommandType CmdType, List<SqlParameter> Params = null)
        {
            int _Out = 0;

            try
            {
                SqlCommand _Cmd = this.PrepareSqlCommand(Stmt, CmdType, Params);

                _Out = _Cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                this.Close(false);
                
                throw new Exception(string.Format(MSG_ERROR, "ExecuteNonQuery", e.Message));
            }
            finally
            {
                if (this._CloseConnectionAfterExecute)
                {
                    this.Close(true);
                }
            }

            return _Out;
        }
        #endregion


        #region Método Post
        public int Post(PostArgs PostArgs)
        {
            int _Out = 0;

            try
            {
                List<SqlParameter> Params = ToSqlParameter(PostArgs.Dto, PostArgs.PostAction, PostArgs.OperationLog);

                SqlCommand _Cmd = this.PrepareSqlCommand(PostArgs.StoredProcedure, CommandType.StoredProcedure, Params);
                _Cmd.ExecuteNonQuery();

                _Out = Convert.ToInt32(Params[0].Value);
            }
            catch (Exception e)
            {
                this.Close(false);

                throw new Exception(string.Format(MSG_ERROR, "Post", e.Message));
            }
            finally
            {
                if (this._CloseConnectionAfterExecute)
                {
                    this.Close(true);
                }

            }

            return _Out;
        }
    }
    #endregion
}
