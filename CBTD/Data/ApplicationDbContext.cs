using System;
using Microsoft.EntityFrameworkCore;

namespace CBTD.Data
{
	public class ApplicationDbContext: DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

		}
	}
}

