using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CRUDFunctionAppDemo.Models;

namespace CRUDFunctionAppDemo.Functions
{
    public static class CreateMarvelHeroFunction
    {
        [FunctionName("CreateMarvelHero")]
        public static async Task<IActionResult> CreateMarvelHero(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = FunctionsSettings.RouteBase)] HttpRequest req,
             [Table(FunctionsSettings.TableName, Connection = FunctionsSettings.AzureWebJobsStorage)] IAsyncCollector<MarvelHeroTableEntity> marvelHeroTable,
            ILogger log)
        {
            log.LogInformation("Create new Marvel hero");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<MarvelHeroModel>(requestBody);

            var marvelHero = new MarvelHero { Name = input.Name, IsDead = input.IsDead };
            await marvelHeroTable.AddAsync(marvelHero.ToTableEntity());
            return new OkObjectResult(marvelHero);
           
        }
    }
}
