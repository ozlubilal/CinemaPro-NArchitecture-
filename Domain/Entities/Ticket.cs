using Core.Persistence.Repositories;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Ticket:Entity<Guid>
{
    public Guid CustomerId { get; set; }
    public Guid FilmSessionId { get; set; }
    public  SeatNumber SeatNumber { get; set; }
    

    public virtual FilmSession? FilmSession { get; set; }
    public virtual Customer?  Customer{ get; set; }

    public Ticket()
    {
        
    }

    public Ticket(Guid id,Guid customerId, Guid filmSessionId)
    {
        Id = id;
        CustomerId = customerId;
        FilmSessionId = filmSessionId;
    }
}
