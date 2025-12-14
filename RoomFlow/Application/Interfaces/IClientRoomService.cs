using RoomFlow.Models;


namespace RoomFlow.Application.Interfaces
{
    public interface IClientRoomService
    {
        Task<List<Room>> GetAvailableRoomsAsync();
    }
}
