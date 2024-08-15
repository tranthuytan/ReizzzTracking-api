using FluentValidation;
using ReizzzTracking.BL.ViewModels.UserViewModel;

namespace ReizzzTracking.BL.Validators.UserValidators
{
    public class UserRegisterValidator : AbstractValidator<UserAddViewModel>
    {
        public UserRegisterValidator()
        {
            RuleFor(u => u.Username)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Please enter a {PropertyName}")
                .NotNull().WithMessage("Please enter a {PropertyName}")
                .Length(4, 20);

            RuleFor(u => u.Password)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Please enter a {PropertyName}")
                .NotNull().WithMessage("Please enter a {PropertyName}")
                .Length(4, 20);

            RuleFor(u => u.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Please enter a {PropertyName}")
                .NotNull().WithMessage("Please enter a {PropertyName}")
                .Length(2,50);

            RuleFor(u => u.PhoneNumber)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Please enter a {PropertyName}")
                .NotNull().WithMessage("Please enter a {PropertyName}")
                .Length(9,10).WithMessage("{PropertyName} must have {MinLength} or {MaxLength} characters");

            //RuleFor(u => u.Gender)
            //    .Cascade(CascadeMode.StopOnFirstFailure)
            //    .NotEmpty().WithMessage("Please enter a {PropertyName}")
            //    .NotNull().WithMessage("Please enter a {PropertyName}")
            //    .ExclusiveBetween(0,2);

            RuleFor(u => u.Birthday)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Please enter a {PropertyName}")
                .NotNull().WithMessage("Please enter a {PropertyName}");
        }
    }
}
