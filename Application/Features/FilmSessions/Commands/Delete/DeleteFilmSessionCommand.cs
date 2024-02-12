using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FilmSessions.Commands.Delete;

public class DeleteFilmSessionCommand:IRequest<DeletedFilmSessionResponse>
{
    public Guid Id { get; set; }
    public class DeleteFilmSessionCommandHandler : IRequestHandler<DeleteFilmSessionCommand, DeletedFilmSessionResponse>
    {
        private readonly IFilmSessionRepository _filmSessionRepository;
        private readonly IMapper _mapper;

        public DeleteFilmSessionCommandHandler(IFilmSessionRepository filmSessionRepository, IMapper mapper)
        {
            _filmSessionRepository = filmSessionRepository;
            _mapper = mapper;
        }

        public async Task<DeletedFilmSessionResponse> Handle(DeleteFilmSessionCommand request, CancellationToken cancellationToken)
        {
            FilmSession? filmSession=await _filmSessionRepository.GetAsync(predicate:f=>f.Id == request.Id);

            await _filmSessionRepository.DeleteAsync(filmSession);

            DeletedFilmSessionResponse response = _mapper.Map<DeletedFilmSessionResponse>(filmSession);
            
            return response;
        }
    }
}
