namespace DataAccess.Data;

using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Category { set; get; }
    public DbSet<Manufacturer> Manufacturer { set; get; }
    public DbSet<Product> Product { set; get; }
    public DbSet<ApplicationUser> ApplicationUser { set; get; }
    
    public DbSet<ShoppingCart> ShoppingCart { set; get; }

}