namespace ReizzzTracking.BL.Services.PermissionService
{
    public interface IUserService
    {
        Task<HashSet<string>> GetPermissionsAsync(long userId);
    }
}
