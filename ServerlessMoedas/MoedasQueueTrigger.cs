using System;
using System.Linq;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using ServerlessMoedas.Models;

namespace ServerlessMoedas
{
    public class MoedasQueueTrigger
    {
        private readonly NHibernate.ISession _sessionNH;

        public MoedasQueueTrigger(NHibernate.ISession sessionNH)
        {
            _sessionNH = sessionNH;
        }

        [FunctionName("MoedasQueueTrigger")]
        public void Run([QueueTrigger("queue-cotacoes", Connection = "AzureWebJobsStorage")]string myQueueItem, ILogger log)
        {
            var cotacao =
                JsonSerializer.Deserialize<Cotacao>(myQueueItem);

            if (!String.IsNullOrWhiteSpace(cotacao.Sigla) &&
                cotacao.Valor.HasValue && cotacao.Valor > 0)
            {
                var dadosCotacao = _sessionNH.Query<Cotacao>()
                        .Where(c => c.Sigla == cotacao.Sigla)
                        .FirstOrDefault();
                if (dadosCotacao != null)
                {
                    dadosCotacao.UltimaCotacao = DateTime.Now;
                    dadosCotacao.Valor = cotacao.Valor;
                    _sessionNH.Save(dadosCotacao);
                    _sessionNH.Flush();
                }

                log.LogInformation($"MoedasQueueTrigger: {myQueueItem}");
            }
            else
                log.LogError($"MoedasQueueTrigger - Erro validação: {myQueueItem}");
        }
    }
}