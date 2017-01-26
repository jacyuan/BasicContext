using System.Configuration;

namespace ApiTest.Infrastructure.Config
{
    public class AppConfiguration : IDatabaseConfiguration
    {
        public string ConnectionString { get; }

        public AppConfiguration()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["PORT-DOTN-21"].ToString();
        }
    }
}