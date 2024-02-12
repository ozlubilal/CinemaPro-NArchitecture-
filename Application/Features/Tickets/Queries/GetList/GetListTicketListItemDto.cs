using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tickets.Queries.GetList;

public class GetListTicketListItemDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FilmName { get; set; }
    public  SeatNumber SeatNumber { get; set; }
    public string SaloonName { get; set; }
    public decimal Price { get; set; }
}
