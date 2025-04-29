using ReizzzTracking.DAL.Entities;
using ReizzzTracking.DAL.Repositories.BaseRepository;

namespace ReizzzTracking.DAL.Repositories.AuthRepository
{
    public interface IAuthRepository : IBaseRepository<User>
    {
        public Task<User?> Login(string loginUsername, string password);
        public Task<User?> GetUserByEmail(string email);
        public Task<User?> GetUserByUsername(string username);
    }
}
