using Microsoft.EntityFrameworkCore;
using ReizzzTracking.DAL.Entities;
using ReizzzTracking.DAL.Repositories.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.DAL.Repositories.AuthRepository
{
    public class AuthRepository : BaseRepository<User>, IAuthRepository
    {
        public AuthRepository(ReizzzTrackingV1Context context) : base(context)
        {
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var result = _dbSet.FirstOrDefault(x => x.Email == email);
            return result;
        }
    }
}
