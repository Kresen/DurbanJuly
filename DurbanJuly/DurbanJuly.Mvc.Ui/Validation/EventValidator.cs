using DurbanJuly.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DurbanJuly.Mvc.Ui.Validation
{
    public class EventValidator : AbstractValidator<EventModel>
    {
        public EventValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(0, 100);
            RuleFor(x => x.TournamentId).NotNull();
            RuleFor(x => x.Number).NotEqual(0);

        }
    }
}
