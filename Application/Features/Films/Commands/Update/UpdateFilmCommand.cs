using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Films.Commands.Update;

public class UpdateFilmCommand : IRequest<UpdatedFilmResponse>, ICacheRemoverRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public string? CacheKey => "";

    public bool BypassCache => false;

    public string? CacheGroupKey => "GetFilms";

    public class UpdateFilmCommandHandler : IRequestHandler<UpdateFilmCommand, UpdatedFilmResponse>
    {
        private readonly IFilmRepository _filmRepository;
        private readonly IMapper _mapper;

        public UpdateFilmCommandHandler(IFilmRepository filmRepository, IMapper mapper)
        {
            _filmRepository = filmRepository;
            _mapper = mapper;
        }

        public async Task<UpdatedFilmResponse> Handle(UpdateFilmCommand request, CancellationToken cancellationToken)
        {
            Film? film = await _filmRepository.GetAsync(predicate: f => f.Id == request.Id, cancellationToken: cancellationToken);

            film = _mapper.Map(request, film);

            await _filmRepository.UpdateAsync(film);

            UpdatedFilmResponse response = _mapper.Map<UpdatedFilmResponse>(film);

            return response;
        }
    }
}
