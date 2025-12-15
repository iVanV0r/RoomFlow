using System.Collections.Generic;
using System.Threading.Tasks;
using RoomFlow.Models;

namespace RoomFlow.Application.Interfaces
{
    public interface IBookingService
    {
        Task<List<Booking>> GetAllAsync();
        Task AddAsync(Booking booking);
    }
}
