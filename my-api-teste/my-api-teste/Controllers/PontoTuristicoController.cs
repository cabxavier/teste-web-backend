using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq;
using BllPontoTuristico = Bll.turismo.PontoTuristico;
using Bll.endereco;
using Dto.endereco;
using Dto.turismo;
using Helper;

namespace my_api_teste.Controllers
{
    public class PontoTuristicoController : ApiController
    {
        [HttpGet]
        [Route("api/pontos-turisticos")]
        public object GetPontoTuristico()
        {
            try
            {
                return (new BllPontoTuristico()).GetPontoTuristico();
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, new HttpError(ex.Message));
            }
        }

        [HttpGet]
        [Route("api/{idEstado}/estados")]
        public object GetEstado(int IdEstado)
        {
            try
            {
                return (new Estado()).GetEstado(IdEstado);
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, new HttpError(ex.Message));
            }
        }

        [HttpGet]
        [Route("api/estados/{idEstado}/cidades")]
        public object GetCidadeGetByIdEstado(int IdEstado)
        {
            try
            {
                return (new Cidade()).GetCidadeGetByIdEstado(IdEstado);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, new HttpError(ex.Message));
            }
        }

        [HttpPost]
        [Route("api/pontos-turisticos/novo")]
        public object PostPontoTuristicoInsert(tpPontoTuristico tpPontoTuristico)
        {
            ConnectionHelper _Conn = new ConnectionHelper();

            bool _Sucesso = false;

            try
            {                
                _Conn.Open(true);

                tpPontoTuristico.DataInclusao = DateTime.Now;
                tpPontoTuristico.IdPontoTuristico = (new BllPontoTuristico(_Conn)).Insert(tpPontoTuristico);

                _Sucesso = true;

                return Request.CreateResponse(HttpStatusCode.OK, "Cadastrado realizado com sucesso!");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, new HttpError(ex.Message));
            }
            finally
            {
                if (_Conn.IsOpened())
                {
                    _Conn.Close(_Sucesso);
                }
            }
        }


        [HttpPut]
        [Route("api/pontos-turisticos/atualizar/{tpPontoTuristico}")]
        public object PutPontoTuristicoUpdate(tpPontoTuristico tpPontoTuristico)
        {
            ConnectionHelper _Conn = new ConnectionHelper();

            bool _Sucesso = false;

            try
            {
                _Conn.Open(true);

                tpPontoTuristico.IdPontoTuristico = (new BllPontoTuristico(_Conn)).Update(tpPontoTuristico);

                _Sucesso = true;

                return Request.CreateResponse(HttpStatusCode.OK, "Alteração realizado com sucesso!");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, new HttpError(ex.Message));
            }
            finally
            {
                if (_Conn.IsOpened())
                {
                    _Conn.Close(_Sucesso);
                }
            }
        }


        [HttpGet]
        [Route("api/{idPontoTuristico}/pontos-turisticos")]
        public object GetCarregarDados(int IdPontoTuristico)
        {
            try
            {
                var _Lista = (new BllPontoTuristico()).GetCarregarDados(IdPontoTuristico);

                return _Lista;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, new HttpError(ex.Message));
            }
        }


        [HttpDelete]
        [Route("api/pontos-turisticos/{idPontoTuristico}/excluir")]
        public object DeletePontoTuristico(int IdPontoTuristico)
        {
            ConnectionHelper _Conn = new ConnectionHelper();

            bool _Sucesso = false;

            try
            {
                _Conn.Open(true);

                (new BllPontoTuristico(_Conn)).Delete(IdPontoTuristico);

                _Sucesso = true;

                return Request.CreateResponse(HttpStatusCode.OK, "Exclusão realizado com sucesso!");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, new HttpError(ex.Message));
            }
            finally
            {
                if (_Conn.IsOpened())
                {
                    _Conn.Close(_Sucesso);
                }
            }
        }
    }    
}
