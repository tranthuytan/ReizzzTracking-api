using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.BL.Errors.Auth
{
    public class AuthError
    {
        public const string RegisterFailMessage = "Fail to create an user";
        public const string DuplicatedUsername = "There's an account with that username. Please try again";
        public const string DuplicatedEmail = "There's an account with that email. Please try again";
    }
}
