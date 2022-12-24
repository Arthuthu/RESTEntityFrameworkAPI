using EntityFrameworkAPITraining.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkAPITraining.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{

		}

		public DbSet<UserModel> Users { get; set; }
	}
}
