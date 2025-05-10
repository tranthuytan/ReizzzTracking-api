using FluentValidation;
using ReizzzTracking.BL.ViewModels.TodoScheduleViewModels;
using ReizzzTracking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.BL.Validators.TodoScheduleValidators
{
    public class TodoScheduleAddValidator : AbstractValidator<TodoScheduleAddViewModel>
    {
        public TodoScheduleAddValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
            RuleFor(td => td.Name)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2);

            RuleFor(td => td.EstimatedTime)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(td => td.TimeUnitId)
                .NotNull()
                .NotEmpty()
                .ExclusiveBetween(1, TimeUnit.GetValues().Count);

            RuleFor(td => td.StartAt)
                .NotNull()
                .NotEmpty()
                .GreaterThanOrEqualTo(DateTime.UtcNow.AddHours(7)).WithMessage("ToDo must have the start time after now in UTC+7");
        }
    }
}
