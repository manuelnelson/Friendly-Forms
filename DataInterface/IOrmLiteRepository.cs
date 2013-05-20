namespace DataInterface
{
    public interface IOrmLiteRepository
    {
        string ConnectionString { get; set; }

        void CreateMissingTables();
    }
}
