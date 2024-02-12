using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Films.Commands.Create;

public class CreateFilmCommand : IRequest<CreatedFilmResponse>
{
    public string Name { get; set; }

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
