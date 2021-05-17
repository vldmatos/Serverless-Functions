namespace Catalog.Settings
{
	public class DatabaseSettings : IDatabaseSettings
	{
		public string CatalogCollectionName { get; set; }
		public string ConnectionString { get; set; }
		public string DatabaseName { get; set; }
	}

	public interface IDatabaseSettings
	{
		string CatalogCollectionName { get; set; }
		string ConnectionString { get; set; }
		string DatabaseName { get; set; }
	}
}
