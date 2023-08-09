namespace DataAccess.Data;

using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext: DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{

	}
		
	public DbSet<Category> Category { set; get; }
	public DbSet<Manufacturer> Manufacturer { set; get; }
		
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Category>().HasData(
			new Category { Id = 1, Name = "Non-Alcoholic Beverages", DisplayOrder = 1 },
			new Category { Id = 2, Name = "Wine", DisplayOrder = 2 },
			new Category { Id = 3, Name = "Snacks", DisplayOrder = 3 }
		);

		modelBuilder.Entity<Manufacturer>().HasData(
			new Manufacturer { Id = 1, Name = "Manufacturer1" },
			new Manufacturer { Id = 2, Name = "Manufacturer2" },
			new Manufacturer { Id = 3, Name = "Manufacturer3" }
		);
			
	}

}