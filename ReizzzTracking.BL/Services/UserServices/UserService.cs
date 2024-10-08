using ReizzzTracking.DAL.Repositories.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.BL.Services.PermissionService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<HashSet<string>> GetPermissionsAsync(long userId)
        {
            return await _userRepository.GetUserPermissionAsync(1);
        }
    }
}
