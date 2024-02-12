using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Films.Commands.Create;

public class CreateFilmCommand : IRequest<CreatedFilmResponse>,ICacheRemoverRequest
{
    public string Name { get; set; }

    public string? CacheKey => "";

    public bool BypassCache => false;

    public string? CacheGroupKey => "GetFilms";

    public class CreateFilmCommandHandler : IRequestHandler<CreateFilmCommand, CreatedFilmResponse>
    {
        private readonly IFilmRepository _filmRepository;
        private readonly IMapper _mapper;

        public CreateFilmCommandHandler(IFilmRepository filmRepository, IMapper mapper)
        {
            _filmRepository = filmRepository;
            _mapper = mapper;
        }

        public async Task<CreatedFilmResponse> Handle(CreateFilmCommand request, CancellationToken cancellationToken)
        {
            Film film = _mapper.Map<Film>(request);
            await _filmRepository.AddAsync(film);

            CreatedFilmResponse response=_mapper.Map<CreatedFilmResponse>(film);
            return response;
        }
    }
}
