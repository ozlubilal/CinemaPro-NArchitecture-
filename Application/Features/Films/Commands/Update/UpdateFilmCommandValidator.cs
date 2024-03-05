using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Films.Commands.Update;

public class UpdateFilmCommandValidator:AbstractValidator<UpdateFilmCommand>
{
    public UpdateFilmCommandValidator()
    {
        //RuleFor(f => f.Name).NotEmpty();
    }
}
