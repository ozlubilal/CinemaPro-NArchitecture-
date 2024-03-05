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
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public Guid FilmSessionId { get; set; }
    public decimal Price { get; set; }
    public List<string> SelectedSeats { get; set; }



}
