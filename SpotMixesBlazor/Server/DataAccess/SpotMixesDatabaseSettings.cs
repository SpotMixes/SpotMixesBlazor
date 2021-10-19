namespace SpotMixesBlazor.Server.DataAccess
{
    public class SpotMixesDatabaseSettings : ISpotMixesDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface ISpotMixesDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}