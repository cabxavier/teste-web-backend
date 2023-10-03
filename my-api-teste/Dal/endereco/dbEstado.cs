using System.Collections.Generic;
using Base;
using Helper;
using Interface;
using Dto.endereco;
using System.Data;

namespace Dal.endereco
{
    public class dbEstado : BaseDal
    {
        #region Constantes
        private const string CS_ESTADO_GET = "stp_EstadoGet";
        #endregion


        #region Constructor
        public dbEstado(ConnectionHelper ConnHelper)
            : base(ConnHelper)
        {
        }
        #endregion


        #region NewDto
        public override IDto NewDto()
        {
            return new tpEstado();
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
            return "Estado";
        }
        #endregion


        #region KeyName
        public override string KeyName()
        {
            return "IdEstado";
        }
        #endregion


        #region DbErrors
        public override Dictionary<string, string> DbErrors()
        {
            Dictionary<string, string> _DBErrors = new Dictionary<string, string>();

            _DBErrors.Add("pk__estado__fbb0edc1850cec4d", "Identificador duplicado");
            _DBErrors.Add("unestado_descricao", "Descrição duplicado");
            _DBErrors.Add("unestado_sigla", "Sigla duplicado");

            return _DBErrors;
        }
        #endregion


        #region LoadObjectProperty
        public override void LoadObjectProperty(IDto Dto, int LoadLevel = 0)
        {

        }
        #endregion


        #region Método GetEstado
        public lstEstado GetEstado()
        {
            lstEstado _ListaEstado = new lstEstado();

            try
            {
                DataTable _Tabela = Conn.ExecuteDataTable(CS_ESTADO_GET, CommandType.StoredProcedure);

                foreach (DataRow Row in _Tabela.Rows)
                {
                    tpEstado _tpEstado = (tpEstado)this.ToDto(Row);

                    _ListaEstado.Add(_tpEstado);
                }
            }
            catch
            {
                throw;
            }

            return _ListaEstado;
        }
        #endregion
    }
}
