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
            RuleLevelCascadeMode = CascadeMode.Stop;
            RuleFor(r => r.Name)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2);

            RuleFor(r => r.StartTimeString)
                .NotNull()
                .NotEmpty()
                .Matches(@"^([01][0-9]|2[0-3]):([0-5][0-9])$")
                    .WithMessage("{PropertyName} must be in the format HH:MM (24-hour format)");

            RuleFor(r => r.IsPublic)
                .NotNull()
                .NotEmpty();

            RuleFor(r => r.CategoryTypeId)
                .NotNull()
                .NotEmpty()
                .ExclusiveBetween(1,2);
        }
    }
}
