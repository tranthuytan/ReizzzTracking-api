using ReizzzTracking.DAL.Entities;
using System.Runtime.CompilerServices;

namespace ReizzzTracking.BL.ViewModels.UserViewModel
{
    public class UserRegisterViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string PhoneNumber { get; set; }
        public byte Gender { get; set; }
        public string Birthday { get; set; }
        public string? Bio { get; set; }

        public UserRegisterViewModel FromUser(User user)
        {
            var result = new UserRegisterViewModel();
            result.Username = user.Username;
            result.Name = user.Name;
            result.Email = user.Email;
            result.PhoneNumber = user.PhoneNumber;
            result.Gender = user.Gender;
            result.Birthday = user.Birthday.ToString("DD/MM/YYYY");
            result.Bio = user.Bio;
            return result;
        }
        public User ToUser(UserRegisterViewModel userViewModel)
        {
            var result = new User
            {
                Username = userViewModel.Username,
                Password = userViewModel.Password,
                Name = userViewModel.Name,
                Email = userViewModel.Email,
                PhoneNumber = userViewModel.PhoneNumber,
                Gender = userViewModel.Gender,
                Birthday = DateOnly.Parse(userViewModel.Birthday),
                Bio = userViewModel.Bio,
            };
            return result;
        }

    }
}
