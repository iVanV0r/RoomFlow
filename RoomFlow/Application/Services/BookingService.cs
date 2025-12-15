using RoomFlow.Models;
using RoomFlow.Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoomFlow.Application.Services
{
    public class BookingService : IBookingService
    {
        private static readonly List<Booking> _bookings = new();

        public Task<List<Booking>> GetAllAsync()
        {
            return Task.FromResult(_bookings);
        }

        public Task AddAsync(Booking booking)
        {
            booking.Id = _bookings.Count + 1;
            _bookings.Add(booking);
            return Task.CompletedTask;
        }
    }
}
