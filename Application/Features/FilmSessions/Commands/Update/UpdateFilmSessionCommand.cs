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

public class UpdateFilmSessionCommand:IRequest<UpdatedFilmSessionResponse>
{
    public Guid Id { get; set; }
    public string FilmName { get; set; }
    public string SaloonName { get; set; }
    public decimal Price { get; set; }
    public DateTime FilmSessionDateTime { get; set; }
    public class UpdateFilmSessionCommandHandler : IRequestHandler<UpdateFilmSessionCommand, UpdatedFilmSessionResponse>
    {
        private readonly IFilmSessionRepository _filmSessionRepository;
        private readonly IMapper _mapper;

        public UpdateFilmSessionCommandHandler(IFilmSessionRepository filmSessionRepository, IMapper mapper)
        {
            _filmSessionRepository = filmSessionRepository;
            _mapper = mapper;
        }

        public async Task<UpdatedFilmSessionResponse> Handle(UpdateFilmSessionCommand request, CancellationToken cancellationToken)
        {
            FilmSession? filmSession = await _filmSessionRepository.GetAsync(predicate: s => s.Id == request.Id, cancellationToken: cancellationToken);

            filmSession = _mapper.Map(request, filmSession);

            await _filmSessionRepository.UpdateAsync(filmSession);

            UpdatedFilmSessionResponse response = _mapper.Map<UpdatedFilmSessionResponse>(filmSession);

            return response;
        }
    }
}
