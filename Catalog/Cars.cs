using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Catalog
{
	public static class Cars
	{
		[FunctionName(nameof(CreateCar))]
		public static async Task<IActionResult> CreateCar(
			[HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest request, 
			ILogger logger)
		{
			logger.LogInformation("HTTP trigger function to create a Car.");

			string requestBody = await new StreamReader(request.Body).ReadToEndAsync();

			var car = JsonConvert.DeserializeObject<Car>(requestBody);
			if (car is null)
				return new BadRequestObjectResult("Invalid Car received");

			return new OkObjectResult($"Car Saved Id {car.Id}!");
		}
	}

	public class Car
	{
		public string Id { get; set; } = Guid.NewGuid().ToString();

		public string Plate { get; set; }

		public string Model { get; set; }

		public decimal Price { get; set; }
	}
}
