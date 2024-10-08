using Microsoft.EntityFrameworkCore;
using ReizzzTracking.DAL.Entities;
using ReizzzTracking.DAL.Repositories.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.DAL.Repositories.UserRepository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ReizzzTrackingV1Context context) : base(context)
        {
        }

        public async Task<HashSet<string>> GetUserPermissionAsync(long userId)
        {
            ICollection<UserRole>[] userRoles = await _dbSet
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                        .ThenInclude(r => r.Permissions)
                .Where(u => u.Id == userId)
                .Select(u => u.UserRoles)
                .ToArrayAsync();

            return userRoles
                    .SelectMany(ur => ur)
                    .Select(ur=>ur.Role)
                    .SelectMany(r=>r.Permissions)
                    .Select(p=>p.Name).ToHashSet();
        }
    }
}
