using DataAccess.Data;
using Infrastructure.Repositories;

namespace Infrastructure;

using Interfaces;
using Models;

public class UnitOfWork : IUnitOfWork
{
    //dependency injection of Data Source
    private readonly ApplicationDbContext _dbContext;

    // Lazy initialized Singleton references.
    private IGenericRepository<Category>? _category;
    private IGenericRepository<Manufacturer>? _manufacturer;
    private IGenericRepository<Product>? _product;
    private IGenericRepository<ApplicationUser>? _applicationUser;
    private IGenericRepository<ShoppingCart>? _shoppingCart;

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Singletons
    public IGenericRepository<Category> Category => _category ??= new GenericRepository<Category>(_dbContext);
    public IGenericRepository<Manufacturer> Manufacturer => _manufacturer ??= new GenericRepository<Manufacturer>(_dbContext);
    public IGenericRepository<Product> Product => _product ??= new GenericRepository<Product>(_dbContext);
    public IGenericRepository<ApplicationUser> ApplicationUser => _applicationUser ??= new GenericRepository<ApplicationUser>(_dbContext);
    public IGenericRepository<ShoppingCart> ShoppingCart => _shoppingCart ??= new GenericRepository<ShoppingCart>(_dbContext);

    public int Commit()
    {
        return _dbContext.SaveChanges();
    }

    public async Task<int> CommitAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}