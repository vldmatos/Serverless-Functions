using Catalog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace Catalog
{
	public static class CarsFunctions
	{
		private const string connectionString = "mongodb://root:AHSB93993@127.0.0.1:27017";
		private const string databaseName = "Catalog";
		private const string collection = "Cars";

		private static CarsService carsService;
		private static CarsService CarsService
		{
			get
			{
				if (carsService is null)
					carsService = new CarsService(connectionString, databaseName, collection);
				
				return carsService;
			}
		}

		[FunctionName(nameof(DeleteCar))]
		public static IActionResult DeleteCar(
			[HttpTrigger(AuthorizationLevel.Function, "delete")] HttpRequest request,
			ILogger logger)
		{
			InformationRequest("HTTP trigger function to delete a Car", request, logger);

			string id = request.Query["id"];
			if (string.IsNullOrEmpty(id))
				return new BadRequestObjectResult("Invalid Id received");

			CarsService.Remove(id);
			
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

			CarsService.Create(car);

			return new OkObjectResult($"Car Saved Id {car.Id}!");
		}

		[FunctionName(nameof(UpdateCar))]
		public static async Task<IActionResult> UpdateCar(
			[HttpTrigger(AuthorizationLevel.Function, "put")] HttpRequest request,
			ILogger logger)
		{
			InformationRequest("HTTP trigger function to update a Car", request, logger);

			string id = request.Query["id"];
			if (string.IsNullOrEmpty(id))
				return new BadRequestObjectResult("Invalid Id received");

			string requestBody = await new StreamReader(request.Body).ReadToEndAsync();

			var car = JsonConvert.DeserializeObject<Car>(requestBody);
			if (car is null)
				return new BadRequestObjectResult("Invalid Car received");

			car.Id = id;
			CarsService.Update(id, car);

			return new OkObjectResult($"Car Updated Id {car.Id}!");
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

			var car = CarsService.Get(id);

			return new OkObjectResult(car);
		}

		[FunctionName(nameof(GetCars))]
		public static IActionResult GetCars(
			[HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest request,
			ILogger logger)
		{
			InformationRequest("HTTP trigger function to delete all Cars", request, logger);

			var cars = CarsService.GetAll();

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
}
