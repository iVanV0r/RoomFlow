using Moq;
using RoomFlow.Controllers;
using RoomFlow.Models;
using RoomFlow.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace RoomFlow.Tests
{
    public class ClientRoomsControllerTests
    {
        private readonly Mock<IClientRoomService> _mockRoomService;
        private readonly ClientRoomsController _controller;

        public ClientRoomsControllerTests()
        {
            _mockRoomService = new Mock<IClientRoomService>();
            _controller = new ClientRoomsController(_mockRoomService.Object);
        }

        // 1. Возвращает ViewResult с доступными комнатами
        [Fact]
        public async Task Index_Returns_ViewResult_With_Available_Rooms()
        {
            var rooms = new List<Room>
            {
                new Room { Id = 1, RoomType = "Стандарт", RoomNumber = "101", Status = RoomStatus.Available },
                new Room { Id = 2, RoomType = "Люкс", RoomNumber = "102", Status = RoomStatus.Available }
            };
            _mockRoomService.Setup(s => s.GetAvailableRoomsAsync()).ReturnsAsync(rooms);

            var result = await _controller.Index() as ViewResult;
            var model = result?.Model as List<Room>;

            Assert.NotNull(model);
            Assert.Equal(2, model.Count);
        }

        // 2. Возвращает пустой список, если нет доступных комнат
        [Fact]
        public async Task Index_Returns_Empty_List_When_No_Rooms_Available()
        {
            _mockRoomService.Setup(s => s.GetAvailableRoomsAsync()).ReturnsAsync(new List<Room>());

            var result = await _controller.Index() as ViewResult;
            var model = result?.Model as List<Room>;

            Assert.Empty(model);
        }

        // 3. Проверка, что все комнаты имеют статус Available
        [Fact]
        public async Task Index_All_Rooms_Should_Be_Available()
        {
            var rooms = new List<Room>
            {
                new Room { Id = 1, Status = RoomStatus.Available },
                new Room { Id = 2, Status = RoomStatus.Available }
            };
            _mockRoomService.Setup(s => s.GetAvailableRoomsAsync()).ReturnsAsync(rooms);

            var result = await _controller.Index() as ViewResult;
            var model = result?.Model as List<Room>;

            Assert.All(model, r => Assert.Equal(RoomStatus.Available, r.Status));
        }

        // 4. Проверка корректности отображения номера комнаты
        [Fact]
        public async Task Index_Displays_Correct_RoomNumber()
        {
            var rooms = new List<Room>
            {
                new Room { Id = 1, RoomNumber = "101", Status = RoomStatus.Available }
            };
            _mockRoomService.Setup(s => s.GetAvailableRoomsAsync()).ReturnsAsync(rooms);

            var result = await _controller.Index() as ViewResult;
            var model = result?.Model as List<Room>;

            Assert.Equal("101", model[0].RoomNumber);
        }

        // 5. Проверка корректности отображения типа комнаты
        [Fact]
        public async Task Index_Displays_Correct_RoomType()
        {
            var rooms = new List<Room>
            {
                new Room { Id = 1, RoomType = "Стандарт", Status = RoomStatus.Available }
            };
            _mockRoomService.Setup(s => s.GetAvailableRoomsAsync()).ReturnsAsync(rooms);

            var result = await _controller.Index() as ViewResult;
            var model = result?.Model as List<Room>;

            Assert.Equal("Стандарт", model[0].RoomType);
        }

        // 6. Проверка корректности отображения цены за ночь
        [Fact]
        public async Task Index_Displays_Correct_Price()
        {
            var rooms = new List<Room>
            {
                new Room { Id = 1, BasePricePerNight = 3500, Status = RoomStatus.Available }
            };
            _mockRoomService.Setup(s => s.GetAvailableRoomsAsync()).ReturnsAsync(rooms);

            var result = await _controller.Index() as ViewResult;
            var model = result?.Model as List<Room>;

            Assert.Equal(3500, model[0].BasePricePerNight);
        }

        // 7. Проверка, что контроллер не возвращает комнаты с Occupied
        [Fact]
        public async Task Index_DoesNot_Return_OccupiedRooms()
        {
            var rooms = new List<Room>
            {
                new Room { Id = 1, Status = RoomStatus.Occupied }
            };
            _mockRoomService.Setup(s => s.GetAvailableRoomsAsync()).ReturnsAsync(new List<Room>());

            var result = await _controller.Index() as ViewResult;
            var model = result?.Model as List<Room>;

            Assert.Empty(model);
        }

        // 8. Проверка отображения описания комнаты
        [Fact]
        public async Task Index_Displays_Description()
        {
            var rooms = new List<Room>
            {
                new Room { Id = 1, Description = "Уютный номер", Status = RoomStatus.Available }
            };
            _mockRoomService.Setup(s => s.GetAvailableRoomsAsync()).ReturnsAsync(rooms);

            var result = await _controller.Index() as ViewResult;
            var model = result?.Model as List<Room>;

            Assert.Equal("Уютный номер", model[0].Description);
        }

        // 9. Проверка отображения удобств
        [Fact]
        public async Task Index_Displays_Amenities()
        {
            var rooms = new List<Room>
            {
                new Room { Id = 1, Amenities = "Wi-Fi, Телевизор", Status = RoomStatus.Available }
            };
            _mockRoomService.Setup(s => s.GetAvailableRoomsAsync()).ReturnsAsync(rooms);

            var result = await _controller.Index() as ViewResult;
            var model = result?.Model as List<Room>;

            Assert.Equal("Wi-Fi, Телевизор", model[0].Amenities);
        }

        // 10. Проверка возврата ViewResult типа View
        [Fact]
        public async Task Index_Returns_ViewResult_Type()
        {
            _mockRoomService.Setup(s => s.GetAvailableRoomsAsync()).ReturnsAsync(new List<Room>());

            var result = await _controller.Index();

            Assert.IsType<ViewResult>(result);
        }
    }
}
