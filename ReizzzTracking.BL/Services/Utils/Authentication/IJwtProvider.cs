using ReizzzTracking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.BL.Services.Utils.Authentication
{
    public interface IJwtProvider
    {
        public string Generate(User user);
    }
}
