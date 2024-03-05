using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tickets.Queries.GetListByDynamic;

public class GetListByDynamicTicketListItemDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FilmName { get; set; } 
    public DateTime FilmSessionDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public List<string> SelectedSeats { get; set; }
    public string SaloonName { get; set; }
    public decimal Price { get; set; }
}
