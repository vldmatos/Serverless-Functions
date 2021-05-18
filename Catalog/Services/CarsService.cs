using Catalog.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Catalog
{
	class CarsService
	{
		#region Fields

		private readonly IMongoCollection<Car> Cars;

		#endregion Fields

		#region Constructors

		public CarsService(string connectionString, string databaseName, string collection)
		{
			var client = new MongoClient(connectionString);
			var database = client.GetDatabase(databaseName);

			Cars = database.GetCollection<Car>(collection);
		}

		#endregion Constructors

		#region Methods
		
		public Car Get(string id) => Cars.Find(find => find.Id == id).FirstOrDefault();

		public IEnumerable<Car> GetAll()
		{
			return Cars.Find(find => true)
					   .Limit(10)
					   .ToList();
		}

		public Car Create(Car car)
		{
			Cars.InsertOne(car);
			return car;
		}

		public void Update(string id, Car car) => Cars.ReplaceOne(find => find.Id == id, car);

		public void Remove(Car car) => Cars.DeleteOne(find => find.Id == car.Id);

		public void Remove(string id) => Cars.DeleteOne(find => find.Id == id);

		#endregion Methods
	}
}
