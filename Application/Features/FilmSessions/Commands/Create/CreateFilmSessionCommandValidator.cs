using Application.Features.FilmSessions.Constants;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FilmSessions.Commands.Create
{
    public class CreateFilmSessionCommandValidator:AbstractValidator<CreateFilmSessionCommand>
    {
        public CreateFilmSessionCommandValidator()
        {
            RuleFor(f => f.SaloonId).NotEmpty();
            RuleFor(f=>f.FilmId).NotEmpty();
            RuleFor(f=>f.FilmSessionDate).NotEmpty();
            RuleFor(f => f.Price).NotEmpty().GreaterThan(0);
            RuleFor(f => f.FilmSessionDate).Must(BeAfterToday).WithMessage(FilmSessionMessages.FilmSessionDateCannotBeforeToday);



        }
        private bool BeAfterToday(DateTime dateTime)
        {
            return dateTime >= DateTime.Today;
        }
    }
}
