using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using Args;
using Interface;
using Helper;
using Util;
using Enum;

namespace Base
{
    public abstract class BaseDal : IDal
    {
        const string SELECT_BY_ID = "SELECT * FROM {0}.{1} WHERE {2} = {3}";
        const string SELECT = "SELECT {0} FROM {1}.{2} {3} {4}";
        const string INSERT = "{0}.stp_{1}Insert";
        const string UPDATE = "{0}.stp_{1}Update";
        const string DELETE = "{0}.stp_{1}Delete";
        const string NEXT_ID = "stp_NextId";

        private ConnectionHelper _ConnHelper = null;
        private string _SchemaName = "";
        private string _TableName = "";
        private string _KeyName = "";
        private Dictionary<string, string> _DbErrors = null;

        public abstract string SchemaName();
        public abstract string TableName();
        public abstract string KeyName();
        public abstract Dictionary<string, string> DbErrors();
        public abstract IDto NewDto();

        protected ConnectionHelper Conn
        {
            get { return this._ConnHelper; }
        }

        public BaseDal(ConnectionHelper ConnHelper)
        {
            if (ConnHelper != null)
            {
                this._ConnHelper = ConnHelper;
            }
            else
            {
                this._ConnHelper = new ConnectionHelper();
            }

            this._SchemaName = SchemaName();
            this._TableName = TableName();
            this._KeyName = KeyName();
            this._DbErrors = DbErrors();
        }

        #region Método ToDto
        public IDto ToDto(DataRow Row, IDto Dto = null)
        {
            IDto _Dto = Dto == null ? this.NewDto() : Dto;

            if (Row != null)
            {
                PropertyInfo[] pInfos = _Dto.GetType().GetProperties();

                foreach (PropertyInfo pInfo in pInfos)
                {
                    if (pInfo.PropertyType.Name.Substring(0, 2) != "tp" &&
                        pInfo.PropertyType.Name.Substring(0, 3) != "lst")
                    {
                        object aux = Row[pInfo.Name];

                        if (aux != null && aux.GetType() != typeof(DBNull))
                            pInfo.SetValue(_Dto, aux, null);
                    }
                }
            }

            return _Dto;
        }
        #endregion


        #region Método SelectById
        public IDto SelectById(int KeyValue, int LoadLevel = 0)
        {
            IDto _Dto = null;

            bool CloseAfterExecute = false;

            if (LoadLevel != 0 && (!this._ConnHelper.IsOpened()))
            {
                this._ConnHelper.Open();

                CloseAfterExecute = true;
            }
            try
            {
                string _Stmt = string.Format(SELECT_BY_ID, this._SchemaName, this._TableName, this._KeyName, KeyValue);

                DataTable _DataTable = this._ConnHelper.ExecuteDataTable(_Stmt, CommandType.Text);

                if (_DataTable.Rows.Count != 0)
                {
                    _Dto = this.ToDto(_DataTable.Rows[0]);

                    var t = _ConnHelper.GetType();

                    if (LoadLevel != 0)
                    {
                        this.LoadObjectProperty(_Dto, LoadLevel);
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (CloseAfterExecute)
                {
                    this._ConnHelper.Close();
                }
            }

            return _Dto;
        }
        #endregion


        #region Método Select
        public DataTable Select(SqlUtil SqlUtil = null, int LoadLevel = 0)
        {
            DataTable _Out = new DataTable();

            string Fields = "*";
            string Where = "";
            string OrderBy = "";

            if (SqlUtil != null)
            {
                if (SqlUtil.Fields.Count > 0)
                {
                    Fields = "";

                    SqlUtil.Fields.ForEach(delegate (string Field)
                    {
                        Fields += (Fields == "") ? Field : ", " + Field;
                    });
                }

                Where = SqlUtil.Where.Text();

                if (Where != "")
                {
                    Where = "WHERE " + Where;
                }

                OrderBy = SqlUtil.Sort.Text();

                if (OrderBy != "")
                {
                    OrderBy = "ORDER BY " + OrderBy;
                }
            }

            bool CloseAfterExecute = false;

            if (LoadLevel != 0 && (this._ConnHelper.IsOpened() == false))
            {
                this._ConnHelper.Open();
                CloseAfterExecute = true;
            }

            try
            {
                string _Stmt = string.Format(SELECT, Fields, this._SchemaName, this._TableName, Where, OrderBy);

                _Out = this._ConnHelper.ExecuteDataTable(_Stmt, System.Data.CommandType.Text);
            }
            catch
            {
                throw;
            }
            finally
            {
                if (CloseAfterExecute)
                {
                    this._ConnHelper.Close();
                }
            }

            return _Out;
        }
        #endregion


        #region Método SelectForList
        public lst SelectForList<lst, dto>(SqlUtil SqlUtil = null, int LoadLevel = 0)
            where lst : class, IList<dto>, new()
            where dto : IDto, new()
        {

            DataTable _DataTable;

            bool CloseAfterExecute = false;

            lst _Out = new lst();

            if (LoadLevel != 0 && (!this._ConnHelper.IsOpened()))
            {
                this._ConnHelper.Open();

                CloseAfterExecute = true;
            }

            try
            {
                _DataTable = this.Select(SqlUtil);

                if (_DataTable != null)
                {
                    int count = 0;

                    foreach (DataRow _DataRow in _DataTable.Rows)
                    {
                        var _Dto = (dto)this.ToDto(_DataRow);

                        if (LoadLevel != 0)
                        {
                            this.LoadObjectProperty((IDto)_Dto, LoadLevel);
                        }

                        _Out.Insert(count, _Dto);

                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (CloseAfterExecute)
                {
                    this._ConnHelper.Close();
                }
            }

            return _Out;
        }
        #endregion


        #region Método SelectOne
        public IDto SelectOne(SqlUtil SqlUtil, int LoadLevel = 0)
        {
            bool CloseAfterExecute = false;

            string Fields = "*";
            string Where = "";
            string OrderBy = "";

            IDto _Out = null;

            DataTable _DataTable = new DataTable();

            if (SqlUtil != null)
            {
                if (SqlUtil.Fields.Count > 0)
                {
                    Fields = "";

                    SqlUtil.Fields.ForEach(delegate (string Field)
                    {
                        Fields += (Fields == "") ? Field : ", " + Field;
                    });
                }

                Where = SqlUtil.Where.Text();

                if (Where != "")
                {
                    Where = "WHERE " + Where;
                }
            }

            if (LoadLevel != 0 && (!this._ConnHelper.IsOpened()))
            {
                this._ConnHelper.Open();

                CloseAfterExecute = true;
            }

            try
            {
                string _Stmt = string.Format(SELECT, Fields, this._SchemaName, this._TableName, Where, OrderBy);

                _DataTable = this._ConnHelper.ExecuteDataTable(_Stmt, System.Data.CommandType.Text);

                if (_DataTable.Rows.Count == 1)
                {
                    _Out = this.ToDto(_DataTable.Rows[0]);

                    if (LoadLevel != 0)
                    {
                        this.LoadObjectProperty(_Out, LoadLevel);
                    }
                }

            }
            catch
            {
                throw;
            }
            finally
            {
                if (CloseAfterExecute)
                {
                    this._ConnHelper.Close();
                }
            }

            return _Out;
        }
        #endregion


        #region Método PostInsert
        public int PostInsert(IDto Dto, bool OperationLog = false)
        {
            try
            {
                PostArgs _PostArgs = new PostArgs();
                _PostArgs.Dto = Dto;
                _PostArgs.OperationLog = OperationLog;
                _PostArgs.StoredProcedure = string.Format(INSERT, this._SchemaName, this._TableName);
                _PostArgs.PostAction = PostAction.Insert;

                return _ConnHelper.Post(_PostArgs);
            }
            catch
            {
                throw;
            }
        }
        #endregion


        #region Método PostUpdate
        public void PostUpdate(IDto Dto, bool OperationLog = false)
        {
            try
            {
                PostArgs _PostArgs = new PostArgs();
                _PostArgs.Dto = Dto;
                _PostArgs.OperationLog = OperationLog;
                _PostArgs.StoredProcedure = string.Format(UPDATE, this._SchemaName, this._TableName);
                _PostArgs.PostAction = PostAction.Update;

                _ConnHelper.Post(_PostArgs);
            }
            catch
            {
                throw;
            }
        }
        #endregion


        #region Método PostDelete
        public void PostDelete(IDto Dto, bool OperationLog = false)
        {
            try
            {
                PostArgs _PostArgs = new PostArgs();
                _PostArgs.Dto = Dto;
                _PostArgs.OperationLog = OperationLog;
                _PostArgs.StoredProcedure = string.Format(DELETE, this._SchemaName, this._TableName);
                _PostArgs.PostAction = PostAction.Delete;

                _ConnHelper.Post(_PostArgs);
            }
            catch
            {
                throw;
            }
        }
        #endregion


        #region Método NextId
        public int NextId()
        {
            int _Out = 0;

            try
            {
                List<SqlParameter> _Params = new List<SqlParameter>();
                _Params.Add(new SqlParameter("Tabela", this._TableName));
                _Params.Add(new SqlParameter("Quebra", (object)null));
                _Params.Add(new SqlParameter("Coluna", this._KeyName));
                _Params.Add(new SqlParameter("Schema", "onf"));
                _Params.Add(new SqlParameter("Id", 0));
                _Params.Last().Direction = ParameterDirection.Output;

                _ConnHelper.ExecuteNonQuery(NEXT_ID, CommandType.StoredProcedure, _Params);
                _Out = Convert.ToInt32(_Params.Last().Value);
            }
            catch
            {
                throw;
            }

            return _Out;
        }
        #endregion


        #region Método NextValue
        public int NextValue(string FieldName, string TextPartition = null)
        {
            int _Out = 0;

            try
            {
                List<SqlParameter> _Params = new List<SqlParameter>();
                _Params.Add(new SqlParameter("Tabela", this._TableName));
                _Params.Add(new SqlParameter("Quebra", string.IsNullOrEmpty(TextPartition) ? (object)null : TextPartition));
                _Params.Add(new SqlParameter("Coluna", FieldName));
                _Params.Add(new SqlParameter("Id", 0));
                _Params.Last().Direction = ParameterDirection.Output;

                _ConnHelper.ExecuteNonQuery(NEXT_ID, CommandType.StoredProcedure, _Params);
                _Out = Convert.ToInt32(_Params.Last().Value);
            }
            catch
            {
                throw;
            }

            return _Out;
        }
        #endregion


        #region Método ExecuteProcedure
        public DataTable ExecuteProcedure(string Stmt, List<SqlParameter> _Parameters)
        {
            return _ConnHelper.ExecuteDataTable(string.Format("{0}.{1}", this._SchemaName, Stmt), CommandType.StoredProcedure, _Parameters);
        }
        #endregion

        public abstract void LoadObjectProperty(IDto _Dto, int LoadLevel = 0);
    }
}