using Microsoft.EntityFrameworkCore;
using RoomFlow.Models;
using System.Collections.Generic;

namespace RoomFlow.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options) { }

		public DbSet<Employee> Employees => Set<Employee>();
	}
}
