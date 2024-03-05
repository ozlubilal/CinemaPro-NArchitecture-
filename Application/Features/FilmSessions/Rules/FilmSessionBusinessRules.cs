using Application.Features.FilmSessions.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FilmSessions.Rules;

public class FilmSessionBusinessRules:BaseBusinessRules
{
    private readonly IFilmSessionRepository _filmSessionRepository;

    public FilmSessionBusinessRules(IFilmSessionRepository filmSessionRepository)
    {
        _filmSessionRepository = filmSessionRepository;
    }
    public async Task SaloonMustBeEmptyFilmTime(FilmSession filmSession)
    {
        FilmSession? result = await _filmSessionRepository.GetAsync(predicate: f => (
        (f.StartTime <= filmSession.EndTime && f.EndTime>=filmSession.StartTime)||
        (filmSession.StartTime <= f.EndTime &&filmSession.EndTime>=f.StartTime ))&&
        f.FilmSessionDate==filmSession.FilmSessionDate && f.SaloonId==filmSession.SaloonId);
        if (result!=null)
        {
            throw new BusinessException(FilmSessionMessages.SaloonIsNotEmpty);
        }
    }

}
