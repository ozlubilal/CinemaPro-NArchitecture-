using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Films.Commands.Create;

public class CreateFilmCommandValidator:AbstractValidator<CreateFilmCommand>
{
    public CreateFilmCommandValidator()
    {
        RuleFor(f=>f.Name).NotEmpty();
    }
}
