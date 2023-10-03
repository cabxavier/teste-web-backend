using System.Collections.Generic;
using Base;
using Interface;

namespace Dto.endereco
{
    public class lstCidade : List<tpCidade> { }

    public class tpCidade : BaseDto, IDto
    {
        public int IdCidade { get; set; }
        public string Descricao { get; set; }
        public int IdEstado { get; set; }

        public tpCidade()
        {
            this.Clear();
        }

        public void Clear()
        {
            this.IdCidade = 0;
            this.Descricao = null;
            this.IdEstado = 0;
        }
    }
}
