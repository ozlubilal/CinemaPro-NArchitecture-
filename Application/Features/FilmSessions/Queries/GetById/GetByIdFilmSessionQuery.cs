using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FilmSessions.Queries.GetById;

public class GetByIdFilmSessionQuery:IRequest<GetByIdFilmSessionResponse>
{
    public Guid Id { get; set; }

    public class GetByIdFilmSessionQueryHandler : IRequestHandler<GetByIdFilmSessionQuery, GetByIdFilmSessionResponse>
    {
        IFilmSessionRepository _filmSessionRepository;
        IMapper _mapper;

        public GetByIdFilmSessionQueryHandler(IFilmSessionRepository filmSessionRepository, IMapper mapper)
        {
            _filmSessionRepository = filmSessionRepository;
            _mapper = mapper;
        }

        public async Task<GetByIdFilmSessionResponse> Handle(GetByIdFilmSessionQuery request, CancellationToken cancellationToken)
        {
            FilmSession? filmSession = await _filmSessionRepository.GetAsync(predicate: f => f.Id == request.Id, withDeleted: true,
                include:f=>f.Include(f=>f.Saloon).Include(f=>f.Film),
                cancellationToken: cancellationToken); 
            GetByIdFilmSessionResponse response=_mapper.Map<GetByIdFilmSessionResponse>(filmSession);

            return response;

        }
    }
}
