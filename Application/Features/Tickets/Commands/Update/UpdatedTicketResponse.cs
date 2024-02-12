using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tickets.Commands.Update;

public class UpdatedTicketResponse
{
    public Guid Id { get; set; }
    public FilmSession FilmSession { get; set; }
    public SeatNumber SeatNumber { get; set; }
}
