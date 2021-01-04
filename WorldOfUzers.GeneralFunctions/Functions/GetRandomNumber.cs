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


        [FunctionName("Ping")]
        public static async Task<IActionResult> PingFunction(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            return new OkObjectResult("It's working!");
        }


        [FunctionName("GetRandomNumber")]
        public static async Task<IActionResult> GetRandomNumberFunction(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req, ILogger log,
            [StorageAccount("woudb")]
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
