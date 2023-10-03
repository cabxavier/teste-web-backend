using Base;
using Helper;
using Interface;
using Dal.turismo;
using Dto.turismo;

namespace Bll.turismo
{
    public class PontoTuristico : BaseBll
    {
        #region Constructor
        public PontoTuristico(ConnectionHelper ConnHelper = null)
            : base(ConnHelper) {
        }
        #endregion


        #region NewDal
        public override IDal NewDal(ConnectionHelper _ConnHelper)
        {
            return new dbPontoTuristico(_ConnHelper);
        }
        #endregion


        #region Método Insert
        public int Insert(tpPontoTuristico tpPontoTuristico)
        {
            try
            {
                tpPontoTuristico.IdPontoTuristico = ((dbPontoTuristico)_Dal).Insert(tpPontoTuristico);
            }
            catch
            {
                throw;
            }

            return tpPontoTuristico.IdPontoTuristico;
        }
        #endregion


        #region Método Update
        public int Update(tpPontoTuristico tpPontoTuristico)
        {
            try
            {
                tpPontoTuristico.IdPontoTuristico = ((dbPontoTuristico)_Dal).Update(tpPontoTuristico);
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
            try
            {
                ((dbPontoTuristico)_Dal).Delete(IdPontoTuristico);
            }
            catch
            {
                throw;
            }
        }
        #endregion


        #region GetPontoTuristico
        public lstPontoTuristicoVisao GetPontoTuristico()
        {
            lstPontoTuristicoVisao _ListaPontoTuristicoVisao = new lstPontoTuristicoVisao();

            try
            {
                _ListaPontoTuristicoVisao = ((dbPontoTuristico)_Dal).GetPontoTuristico();
            }
            catch
            {
                throw;
            }

            return _ListaPontoTuristicoVisao;
        }
        #endregion


        #region GetCarregarDados
        public lstPontoTuristico GetCarregarDados(int IdPontoTuristico)
        {
            lstPontoTuristico _ListaPontoTuristico = new lstPontoTuristico();

            try
            {
                _ListaPontoTuristico = ((dbPontoTuristico)_Dal).GetCarregarDados(IdPontoTuristico);
            }
            catch
            {
                throw;
            }

            return _ListaPontoTuristico;
        }
        #endregion
    }
}
