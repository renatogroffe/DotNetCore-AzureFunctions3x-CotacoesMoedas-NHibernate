using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ServerlessMoedas.Models;

namespace ServerlessMoedas
{
    public class CotacoesHttpTrigger
    {
        private readonly NHibernate.ISession _sessionNH;

        public CotacoesHttpTrigger(NHibernate.ISession sessionNH)
        {
            _sessionNH = sessionNH;
        }

        [FunctionName("CotacoesHttpTrigger")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            string moeda = req.Query["moeda"];
            log.LogInformation($"CotacoesHttpTrigger: {moeda}");

            if (!String.IsNullOrWhiteSpace(moeda))
            {
                return (ActionResult)new OkObjectResult(
                    _sessionNH.Query<Cotacao>()
                        .Where(c => c.Sigla == moeda)
                        .FirstOrDefault()
                    );
            }
            else
            {
                return new BadRequestObjectResult(new
                {
                    Sucesso = false,
                    Mensagem = "Informe uma sigla de moeda válida"
                });
            }
        }
    }
}