using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RoomFlow.Controllers;
using RoomFlow.Models;
using RoomFlow.Application.Interfaces;

namespace RoomFlow.Tests
{
    public class BookingControllerTests
    {
        private readonly Mock<IBookingService> _mockBookingService;
        private readonly BookingController _controller;

        public BookingControllerTests()
        {
            _mockBookingService = new Mock<IBookingService>();
            _controller = new BookingController(_mockBookingService.Object);
        }

        // 1. Возвращает ViewResult со списком бронирований
        [Fact]
        public async Task Index_Returns_ViewResult_With_Bookings()
        {
            _mockBookingService.Setup(s => s.GetAllAsync())
                .ReturnsAsync(new List<Booking> { new Booking() });

            var result = await _controller.Index() as ViewResult;
            var model = result?.Model as List<Booking>;

            Assert.NotNull(model);
            Assert.Single(model);
        }

        // 2. Возвращает пустой список, если бронирований нет
        [Fact]
        public async Task Index_Returns_Empty_List_When_No_Bookings()
        {
            _mockBookingService.Setup(s => s.GetAllAsync())
                .ReturnsAsync(new List<Booking>());

            var result = await _controller.Index() as ViewResult;
            var model = result?.Model as List<Booking>;

            Assert.Empty(model);
        }

        // 3. Проверка имени гостя
        [Fact]
        public async Task Index_Displays_Correct_GuestName()
        {
            _mockBookingService.Setup(s => s.GetAllAsync())
                .ReturnsAsync(new List<Booking>
                {
                    new Booking { GuestName = "Иван Иванов" }
                });

            var result = await _controller.Index() as ViewResult;
            var model = result?.Model as List<Booking>;

            Assert.Equal("Иван Иванов", model[0].GuestName);
        }

        // 4. Проверка email гостя
        [Fact]
        public async Task Index_Displays_Correct_Email()
        {
            _mockBookingService.Setup(s => s.GetAllAsync())
                .ReturnsAsync(new List<Booking>
                {
                    new Booking { GuestEmail = "test@mail.ru" }
                });

            var model = (await _controller.Index() as ViewResult)?.Model as List<Booking>;

            Assert.Equal("test@mail.ru", model[0].GuestEmail);
        }

        // 5. Проверка телефона
        [Fact]
        public async Task Index_Displays_Correct_Phone()
        {
            _mockBookingService.Setup(s => s.GetAllAsync())
                .ReturnsAsync(new List<Booking>
                {
                    new Booking { GuestPhone = "+79999999999" }
                });

            var model = (await _controller.Index() as ViewResult)?.Model as List<Booking>;

            Assert.Equal("+79999999999", model[0].GuestPhone);
        }

        // 6. Проверка дат бронирования
        [Fact]
        public async Task Index_Displays_Correct_Dates()
        {
            var checkIn = DateTime.Today;
            var checkOut = DateTime.Today.AddDays(2);

            _mockBookingService.Setup(s => s.GetAllAsync())
                .ReturnsAsync(new List<Booking>
                {
                    new Booking { CheckInDate = checkIn, CheckOutDate = checkOut }
                });

            var model = (await _controller.Index() as ViewResult)?.Model as List<Booking>;

            Assert.Equal(checkIn, model[0].CheckInDate);
            Assert.Equal(checkOut, model[0].CheckOutDate);
        }

        // 7. Проверка общей стоимости
        [Fact]
        public async Task Index_Displays_Correct_TotalPrice()
        {
            _mockBookingService.Setup(s => s.GetAllAsync())
                .ReturnsAsync(new List<Booking>
                {
                    new Booking { TotalPrice = 5000 }
                });

            var model = (await _controller.Index() as ViewResult)?.Model as List<Booking>;

            Assert.Equal(5000, model[0].TotalPrice);
        }

        // 8. Проверка статуса бронирования
        [Fact]
        public async Task Index_Displays_Correct_Status()
        {
            _mockBookingService.Setup(s => s.GetAllAsync())
                .ReturnsAsync(new List<Booking>
                {
                    new Booking { Status = "Забронировано" }
                });

            var model = (await _controller.Index() as ViewResult)?.Model as List<Booking>;

            Assert.Equal("Забронировано", model[0].Status);
        }

        // 9. Create с валидной моделью делает редирект
        [Fact]
        public async Task Create_Valid_Model_Redirects_To_Index()
        {
            var booking = new Booking
            {
                GuestName = "Test",
                GuestEmail = "test@mail.ru",
                GuestPhone = "123",
                CheckInDate = DateTime.Today,
                CheckOutDate = DateTime.Today.AddDays(1)
            };

            var result = await _controller.Create(booking) as RedirectToActionResult;

            Assert.Equal("Index", result.ActionName);
        }

        // 10. Create с ошибкой возвращает View
        [Fact]
        public async Task Create_Invalid_Model_Returns_View()
        {
            _controller.ModelState.AddModelError("GuestName", "Required");

            var result = await _controller.Create(new Booking()) as ViewResult;

            Assert.NotNull(result);
        }
    }
}
