using ReizzzTracking.DAL.Entities;
using ReizzzTracking.DAL.Repositories.BaseRepository;

namespace ReizzzTracking.DAL.Repositories.UserRepository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public Task<HashSet<string>> GetUserPermissionAsync(long userId);
    }
}
