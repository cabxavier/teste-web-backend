using System.Collections.Generic;
using Base;
using Interface;

namespace Dto.endereco
{
    public class lstEstado : List<tpEstado> { }

    public class tpEstado : BaseDto, IDto
    {
        public int IdEstado { get; set; }
        public string Descricao { get; set; }
        public string Sigla { get; set; }

        public tpEstado()
        {
            this.Clear();
        }

        public void Clear()
        {
            this.IdEstado = 0;
            this.Descricao = null;
            this.Sigla = null;
        }
    }
}
