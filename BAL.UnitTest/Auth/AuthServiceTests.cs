using ReizzzTracking.BL.Services.AuthServices;
using ReizzzTracking.DAL.Common.UnitOfWork;
using ReizzzTracking.DAL.Repositories.AuthRepository;
using Xunit;

namespace BAL.UnitTest.Auth
{
    public class AuthServiceTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        //private readonly IAuthRepository _authRepository = new AuthRepository();
        //private readonly IAuthService _authService = new AuthService(null,null);

        [Fact]
        public async Task RegisterAsync_Should_ReturnError_WhenUsingDuplicatedEmailAddress()
        {
            //Arrange

            //Act

            //Assert

        }
    }
}
