namespace RoadTrafficManagement.Repositories.DatabaseRepository
{
    public interface IDatabaseRepository
    {
        Task CreateDatabase(string databaseName);
    }
}
