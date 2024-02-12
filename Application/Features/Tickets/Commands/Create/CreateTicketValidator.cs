using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tickets.Commands.Create;

public class CreateTicketValidator:AbstractValidator<CreateTicketCommand>
{
    public CreateTicketValidator()
    {
        RuleFor(t=>t.FirstName).NotEmpty();
        RuleFor(t => t.LastName).NotEmpty();
        RuleFor(t => t.PhoneNumber).NotEmpty();
        RuleFor(t => t.FilmSessionId).NotEmpty();
        RuleFor(t => t.SeatNumber).NotEmpty();
    }
}
