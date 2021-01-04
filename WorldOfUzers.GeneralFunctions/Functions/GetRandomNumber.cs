using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WorldOfUzers.GeneralFunctions.DTO;

namespace WorldOfUzers.GeneralFunctions.Functions
{
    public static class GetRandomNumber
    {
        private static Random randomGenerator = new Random();

        [FunctionName("GetRandomNumber")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log,
            [Table("FirstTable")] IAsyncCollector<RandomNumberDTO> table)
        {
            log.LogInformation("Recieved a request for a random number");
            log.LogInformation("No aditional parameter was passed so it's randoming numbers between 0-100");
            RandomNumberDTO randomNumber = new RandomNumberDTO(randomGenerator.Next(0, 101));
            await table.AddAsync(randomNumber);
            log.LogInformation($"The random number generated was: {randomNumber.RandomValue}");
            return new OkObjectResult(randomNumber.RandomValue);
        }
    }
}
