namespace LicenseManagementSystem.Repositories.UnitofWork
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
        void Save();
        int SaveWithCount();
        bool SaveSuccess();
    }
}
