using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FilmSessions.Commands.Create;

public class CreatedFilmSessionResponse
{
    public Guid Id  { get; set; }
    public string FilmName { get; set; }
    public string SaloonName { get; set; }
    public decimal Price { get; set; }
    public DateTime SessionDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
}
