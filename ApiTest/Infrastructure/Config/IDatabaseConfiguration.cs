namespace ApiTest.Infrastructure.Config
{
    public interface IDatabaseConfiguration
    {
        string ConnectionString { get; }
    }
}