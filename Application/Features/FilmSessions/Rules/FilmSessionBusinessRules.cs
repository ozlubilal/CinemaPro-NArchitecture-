using Application.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FilmSessions.Rules;

public class FilmSessionBusinessRules
{
    private readonly IFilmSessionRepository _filmSessionRepository;

    public FilmSessionBusinessRules(IFilmSessionRepository filmSessionRepository)
    {
        _filmSessionRepository = filmSessionRepository;
    }


}
