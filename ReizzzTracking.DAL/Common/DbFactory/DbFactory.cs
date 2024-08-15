using ReizzzTracking.DAL.Entities;

namespace ReizzzTracking.DAL.Common.DbFactory
{
    public class DbFactory : IDbFactory
    {
        private ReizzzTrackingV1Context _context;
        public DbFactory(ReizzzTrackingV1Context context)
        {
            _context = context;
        }
        public ReizzzTrackingV1Context Init()
        {
            if (_context == null)
                _context = new ReizzzTrackingV1Context();
            return _context;
        }
    }
}
