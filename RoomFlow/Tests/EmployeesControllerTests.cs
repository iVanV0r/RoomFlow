using Microsoft.AspNetCore.Mvc;
using Moq;
using RoomFlow.Application.Interfaces;
using RoomFlow.Controllers;
using RoomFlow.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace RoomFlow.Tests
{
	public class EmployeesControllerTests
	{
		private readonly Mock<IEmployeeService> _mockService;
		private readonly EmployeesController _controller;

		public EmployeesControllerTests()
		{
			_mockService = new Mock<IEmployeeService>();
			_controller = new EmployeesController(_mockService.Object);
		}

		// 1. Index возвращает ViewResult с моделью
		[Fact]
		public async Task Index_ReturnsView_WithEmployees()
		{
			_mockService.Setup(s => s.GetAllAsync())
				.ReturnsAsync(new List<Employee>());

			var result = await _controller.Index();

			Assert.IsType<ViewResult>(result);
		}

		// 2. Details возвращает NotFound если employee == null
		[Fact]
		public async Task Details_ReturnsNotFound_WhenEmployeeNotExists()
		{
			_mockService.Setup(s => s.GetByIdAsync(1))
				.ReturnsAsync((Employee)null);

			var result = await _controller.Details(1);

			Assert.IsType<NotFoundResult>(result);
		}

		// 3. Details возвращает ViewResult если employee найден
		[Fact]
		public async Task Details_ReturnsView_WhenEmployeeExists()
		{
			_mockService.Setup(s => s.GetByIdAsync(1))
				.ReturnsAsync(new Employee { Id = 1 });

			var result = await _controller.Details(1);

			Assert.IsType<ViewResult>(result);
		}

		// 4. Create GET просто возвращает представление
		[Fact]
		public void CreateGet_ReturnsView()
		{
			var result = _controller.Create();

			Assert.IsType<ViewResult>(result);
		}

		// 5. Create POST возвращает View если ModelState невалиден
		[Fact]
		public async Task CreatePost_InvalidModel_ReturnsView()
		{
			_controller.ModelState.AddModelError("Name", "Required");

			var result = await _controller.Create(new Employee());

			Assert.IsType<ViewResult>(result);
		}

		// 6. Create POST вызывает AddAsync и перенаправляет
		[Fact]
		public async Task CreatePost_ValidModel_RedirectsToIndex()
		{
			var result = await _controller.Create(new Employee { Id = 1 });

			_mockService.Verify(s => s.AddAsync(It.IsAny<Employee>()), Times.Once);
			Assert.IsType<RedirectToActionResult>(result);
		}

		// 7. Edit GET возвращает NotFound если employee == null
		[Fact]
		public async Task EditGet_ReturnsNotFound_WhenNotExists()
		{
			_mockService.Setup(s => s.GetByIdAsync(1))
				.ReturnsAsync((Employee)null);

			var result = await _controller.Edit(1);

			Assert.IsType<NotFoundResult>(result);
		}

		// 8. Edit POST возвращает NotFound если id != employee.Id
		[Fact]
		public async Task EditPost_IdMismatch_ReturnsNotFound()
		{
			var result = await _controller.Edit(2, new Employee { Id = 1 });

			Assert.IsType<NotFoundResult>(result);
		}

		// 9. Edit POST возвращает View если модель неверна
		[Fact]
		public async Task EditPost_InvalidModel_ReturnsView()
		{
			_controller.ModelState.AddModelError("Name", "Required");

			var result = await _controller.Edit(1, new Employee { Id = 1 });

			Assert.IsType<ViewResult>(result);
		}

		// 10. DeleteConfirmed вызывает DeleteAsync и редиректит
		[Fact]
		public async Task DeleteConfirmed_DeletesAndRedirects()
		{
			var result = await _controller.DeleteConfirmed(1);

			_mockService.Verify(s => s.DeleteAsync(1), Times.Once);
			Assert.IsType<RedirectToActionResult>(result);
		}
	}
}
