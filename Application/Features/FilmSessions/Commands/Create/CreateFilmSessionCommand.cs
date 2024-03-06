using Application.Features.FilmSessions.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FilmSessions.Commands.Create;

public class CreateFilmSessionCommand:IRequest<CreatedFilmSessionResponse>,ISecuredRequest
{
    public Guid FilmId { get; set; }
    public Guid SaloonId { get; set; }
    public decimal Price { get; set; }
    public DateTime FilmSessionDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }

    public string[] Roles => new string[] { "admin" };

    public class CreateFilmSessionCommandHandler : IRequestHandler<CreateFilmSessionCommand, CreatedFilmSessionResponse>
    {
        private readonly IFilmSessionRepository _filmSessionRepository;
        private readonly IMapper _mapper;
        private readonly FilmSessionBusinessRules _filmSessionBusinessRules;

        public CreateFilmSessionCommandHandler(IFilmSessionRepository filmSessionRepository, IMapper mapper, FilmSessionBusinessRules filmSessionBusinessRules)
        {
            _filmSessionRepository = filmSessionRepository;
            _mapper = mapper;
            _filmSessionBusinessRules = filmSessionBusinessRules;
        }

        public async Task<CreatedFilmSessionResponse> Handle(CreateFilmSessionCommand request, CancellationToken cancellationToken)
        {           
            FilmSession filmSession = _mapper.Map<FilmSession>(request);
            await _filmSessionBusinessRules.SaloonMustBeEmptyFilmTime(filmSession);
            await _filmSessionRepository.AddAsync(filmSession);

            CreatedFilmSessionResponse response=_mapper.Map<CreatedFilmSessionResponse>(filmSession);
            return response;
        }
    }
}
