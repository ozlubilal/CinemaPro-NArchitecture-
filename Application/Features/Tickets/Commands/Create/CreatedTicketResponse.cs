using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tickets.Commands.Create;

public class CreatedTicketResponse
{
    public Guid Id { get; set; }
    public Guid FilmSessionId { get; set; }
    public SeatNumber  SeatNumber { get; set; }


}
