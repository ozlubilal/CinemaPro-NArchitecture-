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
    public string SaloonName { get; set; }
    public decimal Price { get; set; }
    public DateTime FilmSessionDateime { get; set; }
}
