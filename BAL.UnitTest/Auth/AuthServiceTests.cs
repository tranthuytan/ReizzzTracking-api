using FluentAssertions;
using NSubstitute;
using ReizzzTracking.BL.Errors.Auth;
using ReizzzTracking.BL.Services.AuthServices;
using ReizzzTracking.BL.Services.PermissionService;
using ReizzzTracking.BL.Services.Utils.Authentication;
using ReizzzTracking.BL.Utils.PasswordHasher;
using ReizzzTracking.BL.ViewModels.ResultViewModels;
using ReizzzTracking.BL.ViewModels.UserViewModel;
using ReizzzTracking.DAL.Common.UnitOfWork;
using ReizzzTracking.DAL.Entities;
using ReizzzTracking.DAL.Repositories.AuthRepository;
using Xunit;

namespace BAL.UnitTest.Auth
{
    public class AuthServiceTests
    {
        private readonly IAuthRepository _authRepositoryMock;
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly IPasswordHasher _passwordHasherMock;
        private readonly IAuthService _authService;
        private readonly IJwtProvider _jwtProviderMock;
        public AuthServiceTests()
        {
            _authRepositoryMock = Substitute.For<IAuthRepository>();
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _passwordHasherMock = Substitute.For<IPasswordHasher>();
            _jwtProviderMock = Substitute.For<IJwtProvider>();
            _authService = new AuthService(_authRepositoryMock,_unitOfWorkMock,_passwordHasherMock, _jwtProviderMock);
        }

        [Fact]
        public async Task RegisterAsync_Should_ReturnError_WhenUsingDuplicatedEmailAddress()
        {
            //Arrange
            var userVM = new UserRegisterViewModel
            {
                Username = "admin",
                Password = "admin",
                Email = "admin@gmail.com",
                Birthday = "2002-08-25",
                Gender = 0,
                Bio = "",
                PhoneNumber = "0123456789",
                Name = "Admin"
            };
            var user = userVM.ToUser(userVM);
            _authRepositoryMock.GetUserByEmail(userVM.Email).Returns(user);
            //Act
            var result = await _authService.Register(userVM);

            //Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Be(AuthError.DuplicatedEmail);
        }
        [Fact]
        public async Task RegisterAsync_Should_ReturnError_WhenUsingDuplicatedUsername()
        {
            //Arrange
            var userVM = new UserRegisterViewModel
            {
                Username = "admin",
                Password = "admin",
                Email = "admin@gmail.com",
                Birthday = "2002-08-25",
                Gender = 0,
                Bio = "",
                PhoneNumber = "0123456789",
                Name = "Admin"
            };
            var user = userVM.ToUser(userVM);
            _authRepositoryMock.GetUserByUsername(userVM.Username).Returns(user);
            //Act
            var result = await _authService.Register(userVM);

            //Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Be(AuthError.DuplicatedUsername);
        }
    }
}
