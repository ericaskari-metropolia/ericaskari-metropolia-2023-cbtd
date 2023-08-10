using DataAccess.Data;
using Infrastructure.Repositories;

namespace Infrastructure;

using Interfaces;
using Models;





public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext; //dependency injection of Data Source

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private IGenericRepository<Category> _Category;
    //ADD ADDITIONAL MODELS HERE

    public IGenericRepository<Category> Category
    {
        get
        {
            if (_Category == null)
            {
                _Category = new GenericRepository<Category>(_dbContext);
            }

            return _Category;
        }
    }

    public IGenericRepository<Manufacturer> Manufacturer => throw new NotImplementedException();

    //ADD ADDITIONAL METHODS FOR EACH MODEL (similar to Category) HERE

    public int Commit()
    {
        return _dbContext.SaveChanges();
    }

    public async Task<int> CommitAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    //additional method added for garbage disposal

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}