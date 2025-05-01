using BL.UnitTest.Services.BaseServiceTest;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using ReizzzTracking.BL.Errors.Auth;
using ReizzzTracking.BL.Services.TodoScheduleServices;
using ReizzzTracking.BL.ViewModels.TodoScheduleViewModels;
using ReizzzTracking.DAL.Common.UnitOfWork;
using ReizzzTracking.DAL.Entities;
using ReizzzTracking.DAL.Repositories.TodoScheduleRepository;
using Xunit;

namespace BL.UnitTest.Services
{
    public class TodoScheduleServiceTest : HttpContextAccessorServiceTest
    {
        private readonly ITodoScheduleRepository _todoScheduleRepositoryMock;
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly ITodoScheduleService _todoScheduleService;
        public TodoScheduleServiceTest()
        {
            _todoScheduleRepositoryMock = Substitute.For<ITodoScheduleRepository>();
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _todoScheduleService = new TodoScheduleService(_todoScheduleRepositoryMock, _unitOfWorkMock, _httpContextAccessorMock);
        }
        [Fact]
        public async Task AddTodoScheduleAsync_Should_ReturnSuccess_WhenValid()
        {
            //Arrange
            TodoScheduleAddViewModel routineAddVM = new TodoScheduleAddViewModel
            {
                Name = "Do task A",
                EstimatedTime = 30,
                TimeUnitId = 1,
            };

            _httpContextAccessorMock.HttpContext.Returns(new DefaultHttpContext { User = claimsPrincipal });

            //Mock _todoScheduleRepository to add a new TodoSchedule with Id = 1
            _todoScheduleRepositoryMock.When(x => x.Add(Arg.Any<TodoSchedule>())).Do(x => x.Arg<TodoSchedule>().Id = 1);

            //Act
            var result = await _todoScheduleService.UserAddToDoSchedule(routineAddVM);

            //Assert
            result.Success.Should().BeTrue();
            _todoScheduleRepositoryMock.Received(1).Add(Arg.Is<TodoSchedule>(td =>
                td.Name == "Do task A" &&
                td.EstimatedTime == 30 &&
                td.Id == 1 &&
                td.TimeUnitId == 1 &&
                td.StartAt == routineAddVM.StartAt &&
                td.AppliedFor == 1 &&
                td.IsDone == false &&
                td.CategoryType == 2
            ));
            await _unitOfWorkMock.Received(1).SaveChangesAsync();
        }
        [Fact]
        public async Task AddTodoScheduleAsync_Should_ReturnError_WhenCannotAccessUserClaim()
        {
            //Arrange
            TodoScheduleAddViewModel routineAddVM = new TodoScheduleAddViewModel
            {
                Name = "Do task A",
                EstimatedTime = 30,
                TimeUnitId = 1,
            };

            //Act
            var result = await _todoScheduleService.UserAddToDoSchedule(routineAddVM);

            //Assert
            result.Success.Should().BeFalse();
            result.Errors.Should().ContainEquivalentOf(AuthError.UserClaimsAccessFailed);
        }
    }
}
