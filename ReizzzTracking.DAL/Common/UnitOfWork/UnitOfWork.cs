using ReizzzTracking.DAL.Entities;

namespace ReizzzTracking.DAL.Common.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private ReizzzTrackingV1Context _context;


        public UnitOfWork(ReizzzTrackingV1Context context)
        {
            _context = context;
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
