using Microsoft.EntityFrameworkCore;
using MVCapp.DBModels;

namespace MVCapp.Models
{
	public class ApplicationDBContext : DbContext
	{
		public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

		public DbSet<Category> Categories { get; set; }

		public DbSet<DBModels.Task> Tasks { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
			var connectionString = configuration.GetConnectionString("DefaultConnection");
			optionsBuilder.UseSqlServer(connectionString);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Category>().ToTable("Category");
			modelBuilder.Entity<DBModels.Task>().ToTable("Task");
		}
	}
}
