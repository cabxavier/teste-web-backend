using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Base;
using Helper;
using Interface;
using Dto.endereco;

namespace Dal.endereco
{
    public class dbCidade : BaseDal
    {
        #region Constantes
        private const string CS_CIDADE_GET_BY_IDCIDADE= "stp_CidadeGetByIdEstado";
        #endregion


        #region Constructor
        public dbCidade(ConnectionHelper ConnHelper)
            : base(ConnHelper)
        {
        }
        #endregion


        #region NewDto
        public override IDto NewDto()
        {
            return new tpCidade();
        }
        #endregion


        #region SchemaName
        public override string SchemaName()
        {
            return "dbo";
        }
        #endregion


        #region TableName
        public override string TableName()
        {
            return "Cidade";
        }
        #endregion


        #region KeyName
        public override string KeyName()
        {
            return "IdCidade";
        }
        #endregion


        #region DbErrors
        public override Dictionary<string, string> DbErrors()
        {
            Dictionary<string, string> _DBErrors = new Dictionary<string, string>();

            _DBErrors.Add("pk__cidade__160879a300599962", "Identificador duplicado");
            _DBErrors.Add("uncidade", "Cidade duplicado");

            return _DBErrors;
        }
        #endregion


        #region LoadObjectProperty
        public override void LoadObjectProperty(IDto Dto, int LoadLevel = 0)
        {

        }
        #endregion


        #region Método GetCidadeGetByIdEstado
        public lstCidade GetCidadeGetByIdEstado(int IdEstado)
        {
            lstCidade _ListaCidade = new lstCidade();

            try
            {
                List<SqlParameter> _Params = new List<SqlParameter>();

                _Params.Add(new SqlParameter("IdEstado", IdEstado));

                DataTable _Tabela = Conn.ExecuteDataTable(CS_CIDADE_GET_BY_IDCIDADE, CommandType.StoredProcedure, _Params);

                foreach (DataRow Row in _Tabela.Rows)
                {
                    tpCidade _tpCidade = (tpCidade)this.ToDto(Row);

                    _ListaCidade.Add(_tpCidade);
                }
            }
            catch
            {
                throw;
            }

            return _ListaCidade;
        }
        #endregion
    }
}
