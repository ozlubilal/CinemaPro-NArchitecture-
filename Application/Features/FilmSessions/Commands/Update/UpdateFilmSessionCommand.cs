using Application.Features.FilmSessions.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FilmSessions.Commands.Update;

public class UpdateFilmSessionCommand : IRequest<UpdatedFilmSessionResponse>
{
    public Guid Id { get; set; }
    public Guid FilmId { get; set; }
    public Guid SaloonId { get; set; }
    public decimal Price { get; set; }
    public DateTime FilmSessionDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
}
public class UpdateFilmSessionCommandHandler : IRequestHandler<UpdateFilmSessionCommand, UpdatedFilmSessionResponse>
{
    private readonly IFilmSessionRepository _filmSessionRepository;
    private readonly IMapper _mapper;
    private readonly FilmSessionBusinessRules _filmSessionBusinessRules;

    public UpdateFilmSessionCommandHandler(IFilmSessionRepository filmSessionRepository, IMapper mapper, FilmSessionBusinessRules filmSessionBusinessRules)
    {
        _filmSessionRepository = filmSessionRepository;
        _mapper = mapper;
        _filmSessionBusinessRules = filmSessionBusinessRules;
    }

    public async Task<UpdatedFilmSessionResponse> Handle(UpdateFilmSessionCommand request, CancellationToken cancellationToken)
    {
        FilmSession? filmSession = await _filmSessionRepository.GetAsync(predicate: s => s.Id == request.Id, cancellationToken: cancellationToken);

        await _filmSessionBusinessRules.SaloonMustBeEmptyFilmTime(filmSession);

        filmSession = _mapper.Map(request, filmSession);

        await _filmSessionRepository.UpdateAsync(filmSession);

        UpdatedFilmSessionResponse response = _mapper.Map<UpdatedFilmSessionResponse>(filmSession);

        return response;
    }
}

