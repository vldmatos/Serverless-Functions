using Catalog.Models;
using Catalog.Settings;
using MongoDB.Driver;
using System.Linq;

namespace Catalog
{
	class CarsService
	{
		#region Fields

		private readonly IMongoCollection<Car> Cars;

		#endregion Fields

		#region Constructors

		public CarsService(IDatabaseSettings settings)
		{
			var client = new MongoClient(settings.ConnectionString);
			var database = client.GetDatabase(settings.DatabaseName);

			Cars = database.GetCollection<Car>(settings.CatalogCollectionName);
		}

		#endregion Constructors

		#region Methods
		
		public Car Get(string id) => Cars.Find(@event => @event.Id == id).FirstOrDefault();

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
