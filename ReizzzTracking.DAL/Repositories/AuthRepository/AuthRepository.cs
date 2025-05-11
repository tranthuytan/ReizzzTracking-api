using Microsoft.EntityFrameworkCore;
using ReizzzTracking.DAL.Entities;
using ReizzzTracking.DAL.Repositories.BaseRepository;

namespace ReizzzTracking.DAL.Repositories.AuthRepository
{
    public class AuthRepository : BaseRepository<User>, IAuthRepository
    {
        public AuthRepository(ReizzzTrackingV1Context context) : base(context)
        {
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            var result = await _dbSet.FirstOrDefaultAsync(x => x.Email == email);
            return result;
        }
        public async Task<User?> GetUserByUsername(string username)
        {
            var result = await _dbSet.FirstOrDefaultAsync(x => x.Username == username);
            return result;
        }

        public async Task<User?> Login(string loginUsername, string password)
        {
            var result = await _dbSet.FirstOrDefaultAsync(u => (u.Username == loginUsername || u.Email == loginUsername) && u.Password == password);
            return result;
        }
    }
}
