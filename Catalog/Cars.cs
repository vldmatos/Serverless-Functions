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

		[FunctionName(nameof(GetCar))]
		public static IActionResult GetCar(
			[HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest request,
			ILogger logger)
		{
			logger.LogInformation("HTTP trigger function to get a Car.");

			string id = request.Query["id"];
			if (string.IsNullOrEmpty(id))
				return new BadRequestObjectResult("Invalid Id received");

			var car = new Car
			{
				Id = id,
				Plate = "FER-8328",
				Model = "Fiat Uno",
				Price = 15000.00M
			};

			return new OkObjectResult(car);
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
