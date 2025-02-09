using BL.UnitTest.Services.BaseServiceTest;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.ObjectPool;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using ReizzzTracking.BL.Errors.Auth;
using ReizzzTracking.BL.Errors.Common;
using ReizzzTracking.BL.Services.RoutineServices;
using ReizzzTracking.BL.ViewModels.Common;
using ReizzzTracking.BL.ViewModels.RoutineCollectionViewModels;
using ReizzzTracking.BL.ViewModels.RoutineViewModel;
using ReizzzTracking.DAL.Common.UnitOfWork;
using ReizzzTracking.DAL.Entities;
using ReizzzTracking.DAL.Repositories.BaseRepository;
using ReizzzTracking.DAL.Repositories.RoutineCollectionRepository;
using ReizzzTracking.DAL.Repositories.RoutineRepository;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using Xunit;

namespace BL.UnitTest.Services
{
    public class RoutineServiceTest : HttpContextAccessorServiceTest
    {
        private readonly IRoutineService _routineService;
        private readonly IRoutineRepository _routineRepositoryMock;
        private readonly IRoutineCollectionRepository _routineCollectionRepositoryMock;
        private readonly IUnitOfWork _unitOfWorkMock;
        public RoutineServiceTest()
        {
            _routineRepositoryMock = Substitute.For<IRoutineRepository>();
            _routineCollectionRepositoryMock = Substitute.For<IRoutineCollectionRepository>();
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _routineService = new RoutineService(_routineRepositoryMock, _routineCollectionRepositoryMock, _httpContextAccessorMock, _unitOfWorkMock);
        }
        #region GetRoutineById
        [Fact]
        public async Task GetRoutineById_Should_ReturnSuccess_WhenRoutineExist()
        {
            //Arrange
            long requestId = 1;
            Routine routine = new Routine
            {
                Id = 1,
                StartTime = "6:30",
                Name = "Test Routine",
                IsPublic = true,
                CreatedBy = 1,
                CategoryType = 1,
                RoutineCollectionId = 1,
            };
            RoutineGetViewModel routineVM = new();
            RoutineGetResultViewModel expectedResult = new RoutineGetResultViewModel
            {
                Success = true,
                Message = null,
                Errors = new List<string>(),
                PaginatedResult = new PaginationGetViewModel<RoutineGetViewModel>
                {
                    IsPaginated = false,
                    TotalRecord = 1,
                    PageSize = null,
                    CurrentPage = null,
                    Data = new List<RoutineGetViewModel>
                    {
                        routineVM.FromRoutine(routine)
                    }
                }
            };
            _routineRepositoryMock.Find(Arg.Is<long>(routine.Id)).Returns(routine);
            //Act
            var actualResult = await _routineService.GetRoutineById(requestId);
            //Assert
            actualResult.Should().BeEquivalentTo(expectedResult);
        }
        [Fact]
        public async Task GetRoutineById_Should_ReturnError_WhenRoutineIsNotExist()
        {

            //Arrange
            long requestId = 1;
            Routine routine = new Routine
            {
                Id = 2,
                StartTime = "6:30",
                Name = "Test Routine",
                IsPublic = true,
                CreatedBy = 1,
                CategoryType = 1,
                RoutineCollectionId = 1,
            };
            RoutineGetViewModel routineVM = new();
            RoutineGetResultViewModel expectedResult = new RoutineGetResultViewModel
            {
                Success = false,
                Errors = new List<string>
                {
                    string.Format(CommonError.NotFoundWithId,"Routine",requestId)
                },
            };
            _routineRepositoryMock.Find(Arg.Is<long>(routine.Id)).Returns(routine);
            //Act
            var actualResult = await _routineService.GetRoutineById(requestId);
            //Assert
            actualResult.Should().BeEquivalentTo(expectedResult);
        }
        #endregion

        #region GetRoutines 
        public static IEnumerable<object[]> GetRoutines_TestData()
        {
            yield return new object[] {1,
                new List<Routine>
                {
                    new Routine
                    {
                        Id = 1,
                        StartTime = "06:30",
                        Name = "Test Routine",
                        IsPublic = true,
                        CreatedBy = 1,
                        CategoryType = 1,
                        RoutineCollectionId = 1,
                    },
                    new Routine
                    {
                        Id = 2,
                        StartTime = "06:40",
                        Name = "Test Routine",
                        IsPublic = true,
                        CreatedBy = 1,
                        CategoryType = 1,
                        RoutineCollectionId = 1,
                    }
                },
                new RoutineGetResultViewModel
                {
                    Success = true,
                    Message = null,
                    Errors = new List<string>(),
                    PaginatedResult = new PaginationGetViewModel<RoutineGetViewModel>
                    {
                        IsPaginated = false,
                        TotalRecord = 2,
                        Data = new List<RoutineGetViewModel>
                        {
                            new RoutineGetViewModel
                            {
                                Id=1,
                                StartTime="06:30",
                                Name="Test Routine",
                                IsPublic=true,
                                CreatedBy=1,
                                CategoryType=1,
                                RoutineCollectionId=1,
                            },
                            new RoutineGetViewModel
                            {
                                Id = 2,
                                StartTime = "06:40",
                                Name = "Test Routine",
                                IsPublic = true,
                                CreatedBy = 1,
                                CategoryType = 1,
                                RoutineCollectionId = 1,
                            }
                        }
                    }
                }
            };
        }

        [Theory]
        [MemberData(nameof(GetRoutines_TestData))]
        public async Task GetRoutines_Should_ReturnSuccess_RoutinesOfRoutineCollection(long routineCollectionId, List<Routine> routines, RoutineGetResultViewModel expected)
        {
            //Arrange
            Expression<Func<Routine, bool>>? expression = x => x.RoutineCollectionId == 1;
            _routineRepositoryMock.GetAll(Arg.Is<Expression<Func<Routine, bool>>>(expr => expr.Compile()(new Routine { RoutineCollectionId = 1 })),
                                          null,
                                          null,
                                          Arg.Is<string[]>(s => s.SequenceEqual(new[] { "StartTime" })),
                                          Arg.Is<bool[]>(s => s.SequenceEqual(new[] { false })))
                                .Returns(Task.FromResult((IEnumerable<Routine>)routines));
            //Act
            var actual = await _routineService.GetRoutines(new GetRoutineRequestViewModel { RoutineCollectionId = routineCollectionId });
            //Assert
            actual.Should().BeEquivalentTo(expected, options => options.WithStrictOrdering());
        }
        #endregion

        #region AddRoutine
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
            //Arrange
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
        #endregion

        #region UpdateOrAddRoutine
        [Fact]
        public async Task UpdateOrAddRoutine_Should_ReturnSuccessAndUpdate_WhenRoutineExist()
        {
            //Arrange
            Routine actualRoutine = new Routine
            {
                Id = 1,
                StartTime = "6:30",
                Name = "Wake up",
                IsPublic = false,
                CreatedBy = null,
                CategoryType = 1,
                RoutineCollectionId = 1
            };
            RoutineUpdateViewModel routineVM = new RoutineUpdateViewModel
            {
                Id = 1,
                StartTimeString = "6:45",
                Name = "Updated Wake up",
                IsPublic = true,
                CreatedBy = 1,
            };

            _routineRepositoryMock.Find(Arg.Is(long.Parse(routineVM.Id.ToString()))).Returns(actualRoutine);
            //Act
            var result = await _routineService.UpdateOrAddRoutine(routineVM);
            //Assert
            result.Success.Should().BeTrue();
        }
        [Fact]
        public async Task UpdateOrAddRoutine_Should_ReturnSuccessAndAdd_WhenIdIsNull()
        {
            //Arrange
            RoutineUpdateViewModel routineVM = new RoutineUpdateViewModel
            {
                Id = null,
                StartTimeString = "6:45",
                Name = "Updated Wake up",
                IsPublic = true,
                CreatedBy = 1,
            };

            //Act
            var result = await _routineService.UpdateOrAddRoutine(routineVM);
            //Assert
            _routineRepositoryMock.Received(1).Add(
                Arg.Is<Routine>(r => r.StartTime == routineVM.StartTimeString &&
                                r.Name == routineVM.Name &&
                                r.IsPublic == routineVM.IsPublic &&
                                r.CreatedBy == routineVM.CreatedBy));
            await _unitOfWorkMock.Received(1).SaveChangesAsync();
            result.Success.Should().BeTrue();
        }
        [Fact]
        public async Task UpdateOrAddRoutine_Should_ReturnError_WhenRoutineIsNotExist()
        {
            //Arrange
            RoutineUpdateViewModel routineVM = new RoutineUpdateViewModel
            {
                Id = 1,
                StartTimeString = "6:45",
                Name = "Updated Wake up",
                IsPublic = true,
                CreatedBy = 1,
            };

            _routineRepositoryMock.Find(Arg.Any<long>()).ReturnsNull();
            //Act
            var result = await _routineService.UpdateOrAddRoutine(routineVM);
            //Assert
            result.Success.Should().BeFalse();
            result.Errors.Should().Contain(string.Format(CommonError.NotFoundWithId, "Routine", routineVM.Id));
        }
        #endregion

        #region DeleteRoutines
        [Fact]
        public async Task DeleteRoutines_Should_ReturnSuccess_WhenRoutineExist()
        {
            //Arrange
            long routineId = 1;
            Routine routine = new Routine
            {
                Id = routineId,
            };
            _routineRepositoryMock.Find(routineId).Returns(routine);

            //Act
            var result = await _routineService.DeleteRoutines([routineId]);

            //Assert
            _routineRepositoryMock.Received(1).Remove(Arg.Is(routine));
            await _unitOfWorkMock.Received(1).SaveChangesAsync();
            result.Success.Should().BeTrue();
        }
        [Fact]
        public async Task DeleteRoutines_Should_ReturnError_WhenRoutineIsNotExist()
        {
            //Arrange
            Routine routine = new Routine
            {
                Id = 1,
            };
            _routineRepositoryMock.Find(routine.Id).ReturnsNull();

            //Act
            var result = await _routineService.DeleteRoutines([routine.Id]);

            //Assert
            result.Success.Should().BeFalse();
            var exception = new ArgumentNullException("routineToDelete", $"There's no routine with that Id = ${routine.Id}");
            result.Errors.Contains(exception.Message);
        }
        #endregion
    }
}
