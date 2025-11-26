using System.ComponentModel.DataAnnotations;

namespace RoomFlow.Models
{
	public class User
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(100)]
		public string Username { get; set; }

		[Required]
		[EmailAddress]
		[MaxLength(256)]
		public string Email { get; set; }

		[Required]
		public string PasswordHash { get; set; }

		[MaxLength(100)]
		public string FirstName { get; set; }

		[MaxLength(100)]
		public string LastName { get; set; }

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime? LastLoginAt { get; set; }
		public bool IsActive { get; set; } = true;

		// Навигационные свойства
		public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
		public virtual ICollection<UserClaim> UserClaims { get; set; } = new List<UserClaim>();
	}

	public class Role
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string Name { get; set; }

		[MaxLength(200)]
		public string Description { get; set; }

		public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
	}

	public class UserRole
	{
		public int UserId { get; set; }
		public virtual User User { get; set; }

		public int RoleId { get; set; }
		public virtual Role Role { get; set; }
	}

	public class UserClaim
	{
		public int Id { get; set; }

		public int UserId { get; set; }
		public virtual User User { get; set; }

		[Required]
		[MaxLength(250)]
		public string ClaimType { get; set; }

		[MaxLength(500)]
		public string ClaimValue { get; set; }
	}
}
