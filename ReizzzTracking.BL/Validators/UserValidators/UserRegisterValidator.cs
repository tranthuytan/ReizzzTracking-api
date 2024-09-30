using FluentValidation;
using ReizzzTracking.BL.ViewModels.UserViewModel;

namespace ReizzzTracking.BL.Validators.UserValidators
{
    public class UserRegisterValidator : AbstractValidator<UserRegisterViewModel>
    {
        public UserRegisterValidator()
        {
            RuleFor(u => u.Username)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .NotNull()
                .Length(4, 20);

            RuleFor(u => u.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .NotNull()
                .Length(4, 20);

            RuleFor(u => u.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .NotNull()
                .Length(2,50);

            RuleFor(u => u.PhoneNumber)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .NotNull()
                .Length(9,10);

            RuleFor(u => u.Gender)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .NotNull()
                .ExclusiveBetween((byte)0,(byte)2);

            RuleFor(u => u.Birthday)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .NotNull();
        }
    }
}
