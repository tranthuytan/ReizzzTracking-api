using BL.UnitTest.Services.BaseServiceTest;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using ReizzzTracking.BL.Errors.Auth;
using ReizzzTracking.BL.Services.RoutineCollectionService;
using ReizzzTracking.BL.ViewModels.Common;
using ReizzzTracking.BL.ViewModels.ResultViewModels.RoutineCollectionViewModel;
using ReizzzTracking.BL.ViewModels.RoutineCollectionViewModels;
using ReizzzTracking.DAL.Common.UnitOfWork;
using ReizzzTracking.DAL.Entities;
using ReizzzTracking.DAL.Repositories.BaseRepository;
using ReizzzTracking.DAL.Repositories.RoutineCollectionRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BL.UnitTest.Services
{
    public class RoutineCollectionTest : HttpContextAccessorServiceTest
    {
        private readonly IRoutineCollectionRepository _routineCollectionRepositoryMock;
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly IRoutineCollectionService _routineCollectionService;
        public RoutineCollectionTest()
        {
            _routineCollectionRepositoryMock = Substitute.For<IRoutineCollectionRepository>();
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _routineCollectionService = new RoutineCollectionService(_routineCollectionRepositoryMock, _unitOfWorkMock, _httpContextAccessorMock);
        }
        [Fact]
        public async Task GetPaginatedRoutineCollection_ReturnSuccess_WhenValid()
        {
            //Arrange
            GetRoutineCollectionRequestViewModel request = new GetRoutineCollectionRequestViewModel
            {
                IsPaginated = true,
                PageSize = 20,
                CurrentPage = 1
            };
            List<RoutineCollection> listRoutineCollections = new List<RoutineCollection>
            {
                new RoutineCollection
                {
                    Id=1,
                    CreatedBy=1,
                    Name = "My morning routine",
                    IsPublic=true,
                    CopyCount=3,
                    CreatedAt = new DateTime(2024,09,09),
                    UpdatedAt= new DateTime(2024,09,10),
                    CreatedByNavigation = new User
                    {
                        Id=1,
                        Name="Tran Thuy Tan"
                    },
                    Routines = new List<Routine>
                    {
                        new Routine
                        {
                            Id=1,
                            StartTime="6:30",
                            Name="Wake up",
                            IsPublic=true,
                            UsedBy=1,
                            CategoryType=1,
                            RoutineCollectionId=1,
                        }
                        ,
                        new Routine
                        {
                            Id=2,
                            StartTime="6:35",
                            Name="Brush teeth",
                            IsPublic=false,
                            UsedBy=1,
                            CategoryType=1,
                            RoutineCollectionId=1,
                        },
                        new Routine
                        {
                            Id=3,
                            StartTime="6:40",
                            Name="Make bed",
                            IsPublic=true,
                            UsedBy=null,
                            CategoryType=1,
                            RoutineCollectionId=1,
                        }
                    }
                }
                ,
                new RoutineCollection
                {
                    Id = 2,
                    CreatedBy=1,
                    Name = "My afternoon routine",
                    IsPublic=true,
                    CopyCount=3,
                    CreatedAt = new DateTime(2024,09,09),
                    UpdatedAt=null,
                    CreatedByNavigation = new User
                    {
                        Id=1,
                        Name="Tran Thuy Tan"
                    },
                    Routines = new List<Routine>
                    {
                        new Routine
                        {
                            Id=4,
                            StartTime="12:00",
                            Name="Eat lunch",
                            IsPublic=true,
                            UsedBy=1,
                            CategoryType=1,
                            RoutineCollectionId=2,
                        },
                        new Routine
                        {
                            Id=5,
                            StartTime="12:30",
                            Name="Quick sleep",
                            IsPublic=false,
                            UsedBy=1,
                            CategoryType=1,
                            RoutineCollectionId=2,
                        }
                    }
                },
                new RoutineCollection
                {
                    Id=3,
                    CreatedBy=1,
                    Name = "My bedtime routine",
                    IsPublic=true,
                    CopyCount=3,
                    CreatedAt = new DateTime(2024,09,09),
                    UpdatedAt=null,
                    CreatedByNavigation = new User
                    {
                        Id=1,
                        Name="Tran Thuy Tan"
                    },
                    Routines = new List<Routine>
                    {
                        new Routine
                        {
                            Id=6,
                            StartTime="22:00",
                            Name="Brush teeth",
                            IsPublic=true,
                            UsedBy=1,
                            CategoryType=1,
                            RoutineCollectionId=3,
                        },
                        new Routine
                        {
                            Id=7,
                            StartTime="22:05",
                            Name="Sleep",
                            IsPublic=true,
                            UsedBy=1,
                            CategoryType=1,
                            RoutineCollectionId=3,
                        }
                    }
                }
            };
            RoutineCollectionGetResultViewModel expectedRoutineCollection = new RoutineCollectionGetResultViewModel
            {
                Success=true,
                Message=null,
                PaginatedResult = new PaginationGetViewModel<RoutineCollectionGetViewModel>
                {
                    IsPaginated = request.IsPaginated,
                    TotalRecord = listRoutineCollections.Count,
                    PageSize = request.PageSize,
                    CurrentPage = request.CurrentPage
                }
            };
            foreach (var routineCollection in listRoutineCollections)
            {
                var routineCollectionVM = new RoutineCollectionGetViewModel();
                expectedRoutineCollection.PaginatedResult.Data.Add(routineCollectionVM.FromRoutineCollection(routineCollection));
            }

            _routineCollectionRepositoryMock.Pagination(Arg.Any<int>(),              // page
                                                        Arg.Any<int>(),              // pageSize
                                                        Arg.Any<Expression<Func<RoutineCollection, bool>>>(), // expression
                                                        Arg.Any<Func<IQueryable<RoutineCollection>, IQueryable<RoutineCollection>>>(), // includeFunc
                                                        Arg.Any<PaginationFilter<RoutineCollection>>(), // filterList
                                                        Arg.Any<string[]>(),         // orderByProperty
                                                        Arg.Any<bool[]>())          //descending
                                            .Returns((listRoutineCollections.Count, listRoutineCollections));

            _httpContextAccessorMock.HttpContext.Returns(new DefaultHttpContext { User = claimsPrincipal });
            //Act
            var actualRoutineCollections = await _routineCollectionService.GetPaginatedRoutineCollection(request);

            //Assert
            actualRoutineCollections.Should().BeEquivalentTo(expectedRoutineCollection);
        }
        [Fact]
        public async Task GetPaginatedRoutineCollection_ReturnError_WhenCannotAccessUserClaim()
        {
            //Arrange
            GetRoutineCollectionRequestViewModel request = new GetRoutineCollectionRequestViewModel
            {
                IsPaginated = true,
                PageSize = 20,
                CurrentPage = 1
            };
            //Act
            var actualRoutineCollections = await _routineCollectionService.GetPaginatedRoutineCollection(request);
            //Assert
            actualRoutineCollections.Errors.Should().Contain(AuthError.UserClaimsAccessFailed);
        }
    }
}
