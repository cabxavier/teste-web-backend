using System.Collections.Generic;
using Base;
using Interface;

namespace Dto.turismo
{
    public class lstPontoTuristicoVisao : List<tpPontoTuristicoVisao> { }

    public class tpPontoTuristicoVisao : BaseDto, IDto
    {
        public int IdPontoTuristico { get;  set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Referencia { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        public tpPontoTuristicoVisao()
        {
            this.Clear();
        }

        public void Clear()
        {
            this.IdPontoTuristico = 0;
            this.Nome = null;
            this.Descricao = null;
            this.Referencia = null;
            this.Cidade = null;
            this.Estado = null;
        }
    }
}
