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
		[FunctionName(nameof(DeleteCar))]
		public static IActionResult DeleteCar(
			[HttpTrigger(AuthorizationLevel.Function, "delete")] HttpRequest request,
			ILogger logger)
		{
			InformationRequest("HTTP trigger function to delete a Car", request, logger);

			string id = request.Query["id"];
			if (string.IsNullOrEmpty(id))
				return new BadRequestObjectResult("Invalid Id received");
			
			return new OkObjectResult($"Car Deleted Id {id}!");
		}

		[FunctionName(nameof(CreateCar))]
		public static async Task<IActionResult> CreateCar(
			[HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest request, 
			ILogger logger)
		{
			InformationRequest("HTTP trigger function to create a Car", request, logger);

			string requestBody = await new StreamReader(request.Body).ReadToEndAsync();

			var car = JsonConvert.DeserializeObject<Car>(requestBody);
			if (car is null)
				return new BadRequestObjectResult("Invalid Car received");

			return new OkObjectResult($"Car Saved Id {car.Id}!");
		}

		[FunctionName(nameof(GetCar))]
		public static IActionResult GetCar(
			[HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest request,
			ILogger logger)
		{
			InformationRequest("HTTP trigger function to get a Car", request, logger);

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


		[FunctionName(nameof(GetCars))]
		public static IActionResult GetCars(
			[HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest request,
			ILogger logger)
		{
			InformationRequest("HTTP trigger function to delete all Cars", request, logger);

			var cars = new Car[] 
			{ 
				new Car
				{
					Plate = "HDE-9382",
					Model = "Celta",
					Price = 18000.00M
				},
				new Car
				{
					Plate = "FER-8328",
					Model = "Fiat Uno",
					Price = 15000.00M
				},
				new Car
				{
					Plate = "GBD-2457",
					Model = "Golf",
					Price = 65000.00M
				}
			};

			return new OkObjectResult(cars);
		}


		private static void InformationRequest(string message, HttpRequest request, ILogger logger)
		{
			logger.LogInformation(@$"{message} 
									Protocol: {request.Protocol}, 
									Path: {request.Path}, 
									Length: {request.ContentLength}");
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
