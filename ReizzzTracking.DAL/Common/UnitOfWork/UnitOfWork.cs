using ReizzzTracking.DAL.Common.DbFactory;
using ReizzzTracking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.DAL.Common.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private ReizzzTrackingV1Context _context;
        private readonly IDbFactory _factory;


        public UnitOfWork(IDbFactory factory)
        {
            _factory = factory;
        }
        public ReizzzTrackingV1Context DbContext
        {
            get
            {
                if (_context == null)
                    _context = _factory.Init();
                return _context;
            }
        }
        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await DbContext.SaveChangesAsync();
        }
    }
}
