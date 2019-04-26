using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CRUDFunctionAppDemo.Functions
{
    public static class GetMarvelHeroFunction
    {
        [FunctionName("GetMarvelHero")]
        public static IActionResult GetMarvelHero(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get",  Route = FunctionsSettings.RouteWithId)] HttpRequest req,
            [Table(FunctionsSettings.TableName, FunctionsSettings.PartitionKey, "{id}", Connection = FunctionsSettings.AzureWebJobsStorage)] MarvelHeroTableEntity marvelHero,
            ILogger log, string id)
        {
            log.LogInformation("Get single marvel hero");
            if (marvelHero == null)
            {
                log.LogInformation("Hero does not exists");
                return new NotFoundResult();
            }
            return new OkObjectResult(marvelHero.ToMarvelHero());




        }
    }
}
