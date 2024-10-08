using ReizzzTracking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.DAL.Common.DbFactory
{
    public interface IDbFactory
    {
        ReizzzTrackingV1Context Init();
    }
}
