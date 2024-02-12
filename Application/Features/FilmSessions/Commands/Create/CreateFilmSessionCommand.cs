using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FilmSessions.Commands.Create;

public class CreateFilmSessionCommand:IRequest<CreatedFilmSessionResponse>
{
    public Guid FilmId { get; set; }
    public Guid SaloonId { get; set; }
    public decimal Price { get; set; }
    public DateTime FilmSessionDateTime { get; set; }
  

    public class CreateFilmSessionCommandHandler : IRequestHandler<CreateFilmSessionCommand, CreatedFilmSessionResponse>
    {
        private readonly IFilmSessionRepository _filmSessionRepository;
        private readonly IMapper _mapper;

        public CreateFilmSessionCommandHandler(IFilmSessionRepository filmSessionRepository, IMapper mapper)
        {
            _filmSessionRepository = filmSessionRepository;
            _mapper = mapper;
        }

        public async Task<CreatedFilmSessionResponse> Handle(CreateFilmSessionCommand request, CancellationToken cancellationToken)
        {
            FilmSession filmSession = _mapper.Map<FilmSession>(request);

            await _filmSessionRepository.AddAsync(filmSession);

            CreatedFilmSessionResponse response=_mapper.Map<CreatedFilmSessionResponse>(filmSession);
            return response;
        }
    }
}
