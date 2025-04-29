using FluentValidation;
using ReizzzTracking.BL.ViewModels.RoutineCollectionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.BL.Validators.RoutineCollectionValidators
{
    public class RoutineCollectionUpdateValidator : AbstractValidator<RoutineCollectionUpdateViewModel>
    {
        public RoutineCollectionUpdateValidator()
        {
            this.RuleLevelCascadeMode = CascadeMode.Stop;
            RuleFor(rc => rc.Name)
                .NotNull()
                .NotEmpty();

            RuleFor(rc => rc.IsPublic)
                .NotNull()
                .NotEmpty();
        }
    }
}
