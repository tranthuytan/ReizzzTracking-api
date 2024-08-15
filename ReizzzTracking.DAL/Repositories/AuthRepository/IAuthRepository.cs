﻿using ReizzzTracking.DAL.Entities;
using ReizzzTracking.DAL.Repositories.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.DAL.Repositories.AuthRepository
{
    public interface IAuthRepository : IBaseRepository<User>
    {
        public Task<User> GetUserByEmail(string email);
    }
}
