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
		public DbSet<Room> Rooms => Set<Room>();
		public DbSet<Client> Clients => Set<Client>();
		public DbSet<Payment> Payments => Set<Payment>();
		public DbSet<AdditionalService> AdditionalServices => Set<AdditionalService>();
		public DbSet<ChangeLog> ChangeLogs => Set<ChangeLog>();
		public DbSet<Reservation> Reservations => Set<Reservation>();
	}
}