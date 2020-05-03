using FluentNHibernate.Mapping;
using ServerlessMoedas.Models;

namespace ServerlessMoedas.Mappings
{
    public class CotacoesMap : ClassMap<Cotacao>
    {
        public CotacoesMap()
        {
            Table("dbo.Cotacoes");

            Id(c => c.Sigla).GeneratedBy.Assigned()
                .Index("PK_Cotacoes").CustomSqlType("char(3)");
            Map(c => c.Nome, "NomeMoeda").Not.Nullable()
                .CustomSqlType("varchar(30)");
            Map(c => c.DataUltimaCotacao, "UltimaCotacao")
                .Not.Nullable().CustomSqlType("datetime");
            Map(c => c.Valor).Not.Nullable()
                .CustomSqlType("numeric(18,4)");
        }
    }
}
