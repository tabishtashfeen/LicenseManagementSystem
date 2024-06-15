namespace LicenseManagementSystem.Repositories.UnitofWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _dbContext;
        public UnitOfWork(DatabaseContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public int SaveWithCount()
        {
            return _dbContext.SaveChanges();
        }

        public bool SaveSuccess()
        {
            return _dbContext.SaveChanges() > 0;
        }
    }
}
