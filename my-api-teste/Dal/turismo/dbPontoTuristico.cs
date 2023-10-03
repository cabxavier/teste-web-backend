using System.Collections.Generic;
using System.Data;
using Base;
using Helper;
using Interface;
using Dto.turismo;
using System.Data.SqlClient;

namespace Dal.turismo
{
    public class dbPontoTuristico : BaseDal
    {
        #region Constantes
        private const string CS_PONTO_TURISTICO_INSERT = "stp_PontoTuristicoInsert";
        private const string CS_PONTO_TURISTICO_DELETE = "stp_PontoTuristicoDelete";
        private const string CS_PONTO_TURISTICO_GET = "stp_PontoTuristicoGet";
        #endregion


        #region Constructor
        public dbPontoTuristico(ConnectionHelper ConnHelper)
            : base(ConnHelper)
        {
        }
        #endregion


        #region NewDto
        public override IDto NewDto()
        {
            return new tpPontoTuristico();
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
            return "PontoTuristico";
        }
        #endregion


        #region KeyName
        public override string KeyName()
        {
            return "IdPontoTuristico";
        }
        #endregion


        #region DbErrors
        public override Dictionary<string, string> DbErrors()
        {
            Dictionary<string, string> _DBErrors = new Dictionary<string, string>();

            _DBErrors.Add("pk__pontotur__fff6d756e7008e28", "Identificador duplicado");
            _DBErrors.Add("unpontoturistico", "Ponto turístico duplicado");
            _DBErrors.Add("unpontoturistico_nome", "Nome duplicado");
            _DBErrors.Add("unpontoturistico_descricao", "Descrição duplicado");           

            return _DBErrors;
        }
        #endregion


        #region LoadObjectProperty
        public override void LoadObjectProperty(IDto Dto, int LoadLevel = 0)
        {

        }
        #endregion


        #region Método Insert
        public int Insert(tpPontoTuristico tpPontoTuristico)
        {
            List<SqlParameter> _Params = new List<SqlParameter>();

            try
            {
                _Params.Add(new SqlParameter("IdPontoTuristico", tpPontoTuristico.IdPontoTuristico));
                _Params.Add(new SqlParameter("Nome", tpPontoTuristico.Nome));
                _Params.Add(new SqlParameter("Descricao", tpPontoTuristico.Descricao));
                _Params.Add(new SqlParameter("Referencia", string.IsNullOrWhiteSpace( tpPontoTuristico.Referencia)?null:tpPontoTuristico.Referencia));
                _Params.Add(new SqlParameter("IdCidade", tpPontoTuristico.IdCidade));                

                _Params[0].Direction = ParameterDirection.Output;

                Conn.ExecuteNonQuery(CS_PONTO_TURISTICO_INSERT, CommandType.StoredProcedure, _Params);

                tpPontoTuristico.IdPontoTuristico = (int)_Params[0].Value;                
            }
            catch
            {
                throw;
            }

            return tpPontoTuristico.IdPontoTuristico;
        }
        #endregion



        #region Método Delete
        public void Delete(int IdPontoTuristico)
        {
            List<SqlParameter> _Params = new List<SqlParameter>();

            try
            {
                _Params.Add(new SqlParameter("IdPontoTuristico", IdPontoTuristico));

                Conn.ExecuteNonQuery(CS_PONTO_TURISTICO_DELETE, CommandType.StoredProcedure, _Params);
            }
            catch
            {
                throw;
            }
        }
        #endregion


        #region Método GetPontoTuristico
        public lstPontoTuristicoVisao GetPontoTuristico()
        {
            lstPontoTuristicoVisao _ListaPontoTuristicoVisao = new lstPontoTuristicoVisao();

            try
            {
                DataTable _Tabela = Conn.ExecuteDataTable(CS_PONTO_TURISTICO_GET, CommandType.StoredProcedure);

                foreach (DataRow Row in _Tabela.Rows)
                {
                    tpPontoTuristicoVisao _tpPontoTuristico = (tpPontoTuristicoVisao)this.ToDto(Row, new tpPontoTuristicoVisao());

                    _ListaPontoTuristicoVisao.Add(_tpPontoTuristico);
                }
            }
            catch
            {
                throw;
            }

            return _ListaPontoTuristicoVisao;
        }
        #endregion
    }
}
