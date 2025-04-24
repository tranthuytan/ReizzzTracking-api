using System.Linq.Expressions;
using ReizzzTracking.DAL.Entities;
using ReizzzTracking.DAL.Repositories.BaseRepository;

namespace ReizzzTracking.DAL.Repositories.UserRoleRepository
{
    public class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(ReizzzTrackingV1Context context) : base(context)
        {
        }
    }
}