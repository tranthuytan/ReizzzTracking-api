using ReizzzTracking.DAL.Entities;

namespace ReizzzTracking.BL.Services.Utils.Authentication
{
    public interface IJwtProvider
    {
        public string Generate(User user);
    }
}
