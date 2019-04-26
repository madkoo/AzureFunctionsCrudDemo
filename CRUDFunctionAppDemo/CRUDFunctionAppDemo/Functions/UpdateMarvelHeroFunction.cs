using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage.Table;
using CRUDFunctionAppDemo.Models;

namespace CRUDFunctionAppDemo.Functions
{
    public static class UpdateMarvelHeroFunction
    {
        [FunctionName("UpdateMarvelHero")]
        public static async Task<IActionResult> UpdateMarvelHero(
           [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = FunctionsSettings.RouteWithId)]HttpRequest req,
    [Table(FunctionsSettings.TableName, Connection = FunctionsSettings.AzureWebJobsStorage)] CloudTable marvelHeroTable,
    ILogger log, string id)
        {

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var updated = JsonConvert.DeserializeObject<MarvelHeroModel>(requestBody);
            var findOperation = TableOperation.Retrieve<MarvelHeroTableEntity>(FunctionsSettings.PartitionKey, id);
            var findResult = await marvelHeroTable.ExecuteAsync(findOperation);
            if (findResult.Result == null)
            {
                return new NotFoundResult();
            }
            var existingRow = (MarvelHeroTableEntity)findResult.Result;

            existingRow.IsDead = updated.IsDead;
            if (!string.IsNullOrEmpty(updated.Name))
            {
                existingRow.Name = updated.Name;
            }

            var replaceOperation = TableOperation.Replace(existingRow);
            await marvelHeroTable.ExecuteAsync(replaceOperation);

            return new OkObjectResult(existingRow.ToMarvelHero());
        }
    }
}
