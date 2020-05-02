using FluentNHibernate.Mapping;
using ServerlessMoedas.Models;

namespace ServerlessMoedas.Mappings
{
    public class CotacoesMap : ClassMap<Cotacao>
    {
        public CotacoesMap()
        {
            Id(c => c.Sigla);
            Map(c => c.NomeMoeda);
            Map(c => c.UltimaCotacao);
            Map(c => c.Valor);
            Table("dbo.Cotacoes");
        }
    }
}