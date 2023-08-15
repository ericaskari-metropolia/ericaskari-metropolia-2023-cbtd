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
    private IGenericRepository<ShoppingCartItem>? _shoppingCartItem;
    private IGenericRepository<OrderDetails>? _orderDetails;
    private IOrderHeaderRepository<OrderHeader>? _orderHeader;

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Singletons
    public IGenericRepository<Category> Category => _category ??= new GenericRepository<Category>(_dbContext);
    public IGenericRepository<Manufacturer> Manufacturer => _manufacturer ??= new GenericRepository<Manufacturer>(_dbContext);
    public IGenericRepository<Product> Product => _product ??= new GenericRepository<Product>(_dbContext);
    public IGenericRepository<ApplicationUser> ApplicationUser => _applicationUser ??= new GenericRepository<ApplicationUser>(_dbContext);
    public IGenericRepository<ShoppingCartItem> ShoppingCartItem => _shoppingCartItem ??= new GenericRepository<ShoppingCartItem>(_dbContext);
    public IGenericRepository<OrderDetails> OrderDetails => _orderDetails ??= new GenericRepository<OrderDetails>(_dbContext);
    public IOrderHeaderRepository<OrderHeader> OrderHeader => _orderHeader ??= new OrderHeaderRepository(_dbContext);

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