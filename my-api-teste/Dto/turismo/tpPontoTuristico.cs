using System;
using System.Collections.Generic;
using Base;
using Interface;

namespace Dto.turismo
{
    public class lstPontoTuristico : List<tpPontoTuristico> { }

    public class tpPontoTuristico : BaseDto, IDto
    {
        public int IdPontoTuristico { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Referencia { get; set; }
        public int IdCidade { get; set; }
        public int IdEstado { get; set; }
        public DateTime DataInclusao { get; set; }

        public tpPontoTuristico()
        {
            this.Clear();
        }

        public void Clear()
        {
            this.IdPontoTuristico = 0;
            this.Nome = null;
            this.Descricao = null;
            this.Referencia = null;
            this.IdCidade = 0;
            this.IdEstado = 0;
            this.DataInclusao = DateTime.MinValue;
        }
    }
}
