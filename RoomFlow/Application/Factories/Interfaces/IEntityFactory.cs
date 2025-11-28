using RoomFlow.Models;

namespace RoomFlow.Application.Factories.Interfaces
{
	public interface IEntityFactory
	{
		Employee CreateEmployee(string fullName, 
			string position, 
			decimal salary,
			DateTime? hireDate = null, 
			string? email = null, 
			string? phone = null);

		Client CreateClient(string fullName, 
			string email, 
			DateTime dateOfBorn, 
			string gender, 
			long passport, 
			string? phone = null);

		Booking CreateBooking(int roomId, 
			string guestName, 
			string guestPhone, 
			DateTime checkInDate, 
			DateTime checkOutDate,
			decimal totalPrice, 
			string status);

		Room CreateRoom(string roomNumber, 
			string roomType, 
			int floor, 
			int capasity, 
			decimal basePricePerNight, 
			RoomStatus status,
			string? description, 
			string? amenities);

		Payment CreatePayment(int bookingId, 
			decimal amount, 
			PaymentMethod paymentMethod,
			PaymentStatus paymentStatus,
			DateTime paymentDate,
			string? transactionId,
			string? notes,
			string curency,
			decimal exchangeRate,
			decimal originalAmount,
			string? paymentGateway);

		AdditionalService CreateAdditionalService(string name, 
			string description,
			string category,
			decimal price,
			string priceType,
			bool isAvailable,
			int? durationMinutes,
			bool requiresReservation,
			DateTime createdAt);

		User CreateUser(string username, 
			string email, 
			string passwordHash, 
			string firstName, 
			string lastName,
			DateTime createdAt,
			DateTime? lastLoginAt,
			bool isActive);
	}
}
