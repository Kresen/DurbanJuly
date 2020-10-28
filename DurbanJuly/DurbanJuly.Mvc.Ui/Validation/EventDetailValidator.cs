using DurbanJuly.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DurbanJuly.Mvc.Ui.Validation
{
    public class EventDetailValidator : AbstractValidator<EventDetailModel>
    {
        public EventDetailValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(0, 50);
            RuleFor(x => x.Number).NotEqual(0);
            RuleFor(x => x.FinishingPosition).NotEqual(0);
        }
    }
}
