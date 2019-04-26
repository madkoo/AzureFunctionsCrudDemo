using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage.Table;
using System.Linq;

namespace CRUDFunctionAppDemo.Functions
{
    public static class GetMarvelHeroesFunction
    {
        [FunctionName("GetMarvelHeroes")]
        public static async Task<IActionResult> GetMarvelHeroes(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = FunctionsSettings.RouteBase)]HttpRequest req,
             [Table(FunctionsSettings.TableName, Connection = FunctionsSettings.AzureWebJobsStorage)] CloudTable marvelHeroTable,
            ILogger log)
        {
            log.LogInformation("Get list of marvel heroes.");

            var query = new TableQuery<MarvelHeroTableEntity>();
            var segment = await marvelHeroTable.ExecuteQuerySegmentedAsync(query, null);
            return new OkObjectResult(segment.Select(Mappings.ToMarvelHero));
        }
    }
}
