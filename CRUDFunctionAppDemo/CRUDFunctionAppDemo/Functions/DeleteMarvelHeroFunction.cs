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
using Microsoft.WindowsAzure.Storage;

namespace CRUDFunctionAppDemo.Functions
{
    public static class DeleteMarvelHeroFunction
    {
        [FunctionName("DeleteMarvelHero")]
        public static async Task<IActionResult> DeleteMarvelHero(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = FunctionsSettings.RouteWithId)] HttpRequest req,
               [Table(FunctionsSettings.TableName, Connection = FunctionsSettings.AzureWebJobsStorage)] CloudTable marvelHeroTable,
            ILogger log, string id)
        {
            log.LogInformation("Delete marvel hero.");

            var deleteOperation = TableOperation.Delete(new TableEntity { RowKey = id, PartitionKey = FunctionsSettings.PartitionKey, ETag = "*" });
            try
            {
                var deleteResult = await marvelHeroTable.ExecuteAsync(deleteOperation);
            }
            catch (StorageException ex) when (ex.RequestInformation.HttpStatusCode == 404)
            {
                return new NotFoundResult();
                
            }
            return new OkResult();
          
        }
    }
}
