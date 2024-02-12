using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Saloons.Commands.Create;

public class CreateSaloonCommandValidator:AbstractValidator<CreateSaloonCommand>
{
    public CreateSaloonCommandValidator()
    {
        RuleFor(s => s.Name).NotEmpty();
    }
}
