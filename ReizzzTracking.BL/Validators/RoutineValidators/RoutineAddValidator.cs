using FluentValidation;
using ReizzzTracking.BL.ViewModels.RoutineViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.BL.Validators.RoutineValidators
{
    public class RoutineAddValidator : AbstractValidator<RoutineAddViewModel>
    {
        public RoutineAddValidator()
        {
            RuleFor(r => r.Name)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2);

            RuleFor(r => r.StartTimeString)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .Matches(@"^([01][0-9]|2[0-3]):([0-5][0-9])$")
                    .WithMessage("{PropertyName} must be in the format HH:MM (24-hour format)");

            RuleFor(r => r.IsPublic)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty();

            RuleFor(r => r.CategoryTypeId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .ExclusiveBetween(1,2);
        }
    }
}
