using Base;
using Helper;
using Interface;
using Dal.endereco;
using Dto.endereco;

namespace Bll.endereco
{
    public class Estado : BaseBll
    {
        #region Constructor
        public Estado(ConnectionHelper ConnHelper = null)
            : base(ConnHelper) {
        }
        #endregion


        #region NewDal
        public override IDal NewDal(ConnectionHelper _ConnHelper)
        {
            return new dbEstado(_ConnHelper);
        }
        #endregion

        #region GetEstado
        public lstEstado GetEstado()
        {
            lstEstado _ListaEstado = new lstEstado();

            try
            {
                _ListaEstado = ((dbEstado)_Dal).GetEstado();
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
