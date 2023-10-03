using Base;
using Helper;
using Interface;
using Dal.endereco;
using Dto.endereco;

namespace Bll.endereco
{
    public class Cidade : BaseBll
    {
        #region Constructor
        public Cidade(ConnectionHelper ConnHelper = null)
            : base(ConnHelper) {
        }
        #endregion


        #region NewDal
        public override IDal NewDal(ConnectionHelper _ConnHelper)
        {
            return new dbCidade(_ConnHelper);
        }
        #endregion


        #region Método GetCidadeGetByIdEstado
        public lstCidade GetCidadeGetByIdEstado(int IdEstado)
        {
            lstCidade _ListaCidade = new lstCidade();

            try
            {
                _ListaCidade = ((dbCidade)_Dal).GetCidadeGetByIdEstado(IdEstado);
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
