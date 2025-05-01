namespace ReizzzTracking.BL.Errors.Auth
{
    public class AuthError
    {
        public const string RegisterFailMessage = "Fail to create an user";
        public const string DuplicatedUsername = "There's an account with that username. Please try again";
        public const string DuplicatedEmail = "There's an account with that email. Please try again";
        public const string UserClaimsAccessFailed = "Can't get user's claim. Make sure the user has been logged in";
    }
}
