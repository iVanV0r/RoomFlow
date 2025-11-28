using RoomFlow.Application.Factories.Interfaces;
using RoomFlow.Models;

namespace RoomFlow.Application.Factories
{
	public class EntityFactory : IEntityFactory
	{
		public Employee CreateEmployee(string fullName, string position, decimal salary, DateTime? hireDate = null, string? email = null, string? phone = null)
		{
			return new Employee
			{
				FullName = fullName.Trim(),
				Position = position.Trim(),
				Salary = salary,
				HireDate = hireDate ?? DateTime.Now.Date,
				Email = string.IsNullOrWhiteSpace(email) ? null : email.Trim(),
				Phone = string.IsNullOrWhiteSpace(phone) ? null : phone.Trim()
			};
		}

		public Client CreateClient(string fullName, string email, DateTime dateOfBorn, string gender, long passport, string? phone = null)
		{
			return new Client
			{
				FullName = fullName.Trim(),
				Email = email?.Trim(),
				DateOfBorn = dateOfBorn,
				Gender = gender,
				Passport = passport,
				Phone = string.IsNullOrWhiteSpace(phone) ? null : phone.Trim()
			};
		}

		public Booking CreateBooking(int roomId, string guestName, string guestPhone, DateTime checkInDate, DateTime checkOutDate, decimal totalPrice, string status)
		{
			return new Booking
			{
				RoomId = roomId,
				GuestName = guestName.Trim(),
				GuestPhone = string.IsNullOrWhiteSpace(guestPhone) ? null : guestPhone.Trim(),
				CheckInDate = checkInDate,
				CheckOutDate = checkOutDate,
				TotalPrice = totalPrice,
				Status = status
			};
		}

		public Room CreateRoom(string roomNumber, string roomType, int floor, int capacity, decimal basePricePerNight, RoomStatus status, string? description, string? amenities)
		{
			return new Room
			{
				RoomNumber = roomNumber.Trim(),
				RoomType = roomType.Trim(),
				Floor = floor,
				Capacity = capacity,
				BasePricePerNight = basePricePerNight,
				Status = status,
				Description = description?.Trim(),
				Amenities = amenities?.Trim(),
				CreatedDate = DateTime.Now
			};
		}

		public Payment CreatePayment(int bookingId, decimal amount, PaymentMethod paymentMethod, PaymentStatus paymentStatus, DateTime paymentDate, string? transactionId, string? notes, string currency, decimal exchangeRate, decimal originalAmount, string? paymentGateway)
		{
			return new Payment
			{
				BookingId = bookingId,
				Amount = amount,
				PaymentMethod = paymentMethod,
				Status = paymentStatus,
				PaymentDate = paymentDate,
				TransactionId = string.IsNullOrWhiteSpace(transactionId) ? null : transactionId.Trim(),
				Notes = string.IsNullOrWhiteSpace(notes) ? null : notes.Trim(),
				Currency = currency,
				ExchangeRate = exchangeRate,
				OriginalAmount = originalAmount,
				PaymentGateway = string.IsNullOrWhiteSpace(paymentGateway) ? null : paymentGateway.Trim()
			};
		}

		public AdditionalService CreateAdditionalService(string name, string description, string category, decimal price, string priceType, bool isAvailable, int? durationMinutes, bool requiresReservation, DateTime createdAt)
		{
			return new AdditionalService
			{
				Name = name.Trim(),
				Description = description?.Trim(),
				Category = category.Trim(),
				Price = price,
				PriceType = priceType.Trim(),
				IsAvailable = isAvailable,
				DurationMinutes = durationMinutes,
				RequiresReservation = requiresReservation,
				CreatedAt = createdAt
			};
		}

		public User CreateUser(string username, string email, string passwordHash, string firstName, string lastName, DateTime createdAt, DateTime? lastLoginAt, bool isActive)
		{
			return new User
			{
				Username = username.Trim(),
				Email = email?.Trim(),
				PasswordHash = passwordHash,
				FirstName = firstName.Trim(),
				LastName = lastName.Trim(),
				CreatedAt = createdAt,
				LastLoginAt = lastLoginAt,
				IsActive = isActive
			};
		}
	}
}
