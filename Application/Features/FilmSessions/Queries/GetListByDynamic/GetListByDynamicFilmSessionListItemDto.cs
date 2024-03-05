using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FilmSessions.Queries.GetListByDynamic;

public class GetListByDynamicFilmSessionListItemDto
{
    public Guid Id { get; set; }
    public string FilmName { get; set; }
    public string ImageUrl { get; set; }
    public string SaloonName { get; set; }
    public decimal Price { get; set; }
    public DateTime FilmSessionDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }


    public string FormattedDate => FilmSessionDate.ToShortDateString();
    public string FormattedStartTime => StartTime.ToString("hh\\:mm");
    public string FormattedEndTime => EndTime.ToString("hh\\:mm");
}

