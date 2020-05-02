using System;

namespace ServerlessMoedas.Models
{
    public class Cotacao
    {
        public virtual string Sigla { get; set; }
        public virtual string NomeMoeda { get; set; }
        public virtual DateTime? UltimaCotacao   { get; set; }
        public virtual decimal? Valor { get; set; }
    }
}