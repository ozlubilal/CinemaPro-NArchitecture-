using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class FilmSession:Entity<Guid>
{
    public Guid FilmId { get; set; }
    public Guid SaloonId { get; set; }
    public DateTime FilmSessionDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public decimal Price { get; set; }
    public virtual Film? Film { get; set; }
    public virtual Saloon? Saloon { get; set; }
    
    public FilmSession()
    {
        
    }
    public FilmSession(Guid id, Guid filmId, Guid saloonId, decimal price, DateTime sessionDate, TimeSpan startTime, TimeSpan endTime)
    {
        Id = id;
        FilmId = filmId;
        SaloonId = saloonId;
        Price = price;
        FilmSessionDate = sessionDate;
        StartTime = startTime;
        EndTime = endTime;
    }
}
