using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using ReizzzTracking.BL.Errors.Auth;
using ReizzzTracking.BL.Services.RoutineServices;
using ReizzzTracking.BL.ViewModels.RoutineCollectionViewModels;
using ReizzzTracking.BL.ViewModels.RoutineViewModel;
using ReizzzTracking.DAL.Common.UnitOfWork;
using ReizzzTracking.DAL.Entities;
using ReizzzTracking.DAL.Repositories.RoutineCollectionRepository;
using ReizzzTracking.DAL.Repositories.RoutineRepository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Xunit;

namespace BL.UnitTest.Services
{
    public class RoutineServiceTest
    {
        private readonly IRoutineService _routineService;
        private readonly IRoutineRepository _routineRepositoryMock;
        private readonly IRoutineCollectionRepository _routineCollectionRepositoryMock;
        private readonly IHttpContextAccessor _httpContextAccessorMock;
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly ClaimsPrincipal claimsPrincipal;
        public RoutineServiceTest()
        {
            _routineRepositoryMock = Substitute.For<IRoutineRepository>();
            _routineCollectionRepositoryMock = Substitute.For<IRoutineCollectionRepository>();
            _httpContextAccessorMock = Substitute.For<IHttpContextAccessor>();
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _routineService = new RoutineService(_routineRepositoryMock, _routineCollectionRepositoryMock, _httpContextAccessorMock, _unitOfWorkMock);

            //Mock user's claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,"1")
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            claimsPrincipal = new ClaimsPrincipal(identity);
        }
        [Fact]
        public async Task AddRoutineAsync_Should_ReturnSuccess_WhenRoutineRoutineCollectionIdIsNull()
        {
            //Arrange
            RoutineAddViewModel routineAddVM = new RoutineAddViewModel
            {
                Name = "Test Routine",
                StartTimeString = "6:00",
                IsPublic = true,
                CategoryTypeId = 1,
            };

            _httpContextAccessorMock.HttpContext.Returns(new DefaultHttpContext { User = claimsPrincipal });

            //Mock _routineCollectionRepository to add a new routine collection with Id = 1
            _routineCollectionRepositoryMock.When(x => x.Add(Arg.Any<RoutineCollection>())).Do(x => x.Arg<RoutineCollection>().Id = 1);

            //Act
            var result = await _routineService.AddRoutine(routineAddVM);

            //Assert
            result.Success.Should().BeTrue();
            _routineCollectionRepositoryMock.Received(1).Add(Arg.Is<RoutineCollection>(rc =>
                rc.Name == "New Routine Collection" &&
                rc.CreatedBy == 1 &&
                rc.Id == 1
            ));
            await _unitOfWorkMock.Received(2).SaveChangesAsync();
            _routineRepositoryMock.Received(1).Add(Arg.Is<Routine>(r => r.RoutineCollectionId == 1));

        }
        [Fact]
        public async Task AddRoutineAsync_Should_ReturnSuccess_WhenRoutineRoutineCollectionIdIsNotNull()
        {
            //Arrange
            var routineAddVM = new RoutineAddViewModel
            {
                Name = "Test Routine",
                StartTimeString = "6:00",
                IsPublic = true,
                CategoryTypeId = 1,
                RoutineCollectionId = 1
            };

            _httpContextAccessorMock.HttpContext.Returns(new DefaultHttpContext { User = claimsPrincipal });

            //Act
            await _routineService.AddRoutine(routineAddVM);
            //Assert
            await _unitOfWorkMock.Received(1).SaveChangesAsync();
            _routineRepositoryMock.Received(1).Add(Arg.Is<Routine>(r =>
                r.Name == "Test Routine" &&
                r.StartTime == "6:00" &&
                r.IsPublic == true &&
                r.CategoryType == 1 &&
                r.RoutineCollectionId == 1
            ));
        }
        [Fact]
        public async Task AddRoutineAsync_Should_ReturnError_WhenCanNotAccessUserClaim()
        {
            //Arrangevar
            RoutineAddViewModel routineAddVM = new RoutineAddViewModel
            {
                Name = "Test Routine",
                StartTimeString = "6:00",
                IsPublic = true,
                CategoryTypeId = 1,
                RoutineCollectionId = 1
            };
            _httpContextAccessorMock.HttpContext.ReturnsNull();
            //Act
            var result = await _routineService.AddRoutine(routineAddVM);
            //Assert
            result.Success.Should().BeFalse();
            result.Errors.Should().Contain(AuthError.UserClaimsAccessFailed);
        }
    }
}
