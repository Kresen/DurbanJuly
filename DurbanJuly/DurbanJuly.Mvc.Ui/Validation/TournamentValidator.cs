using DurbanJuly.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DurbanJuly.Mvc.Ui.Validation
{
    public class TournamentValidator : AbstractValidator<TournamentModel>
    {
        public TournamentValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(0, 200);
        }
    }
}
