using Application.Features.Films.Commands.Delete;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Films.Commands.Delete;

public class DeleteFilmCommand : IRequest<DeletedFilmResponse>,ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new string[] {"admin"};

    public class DeleteFilmCommandHandler : IRequestHandler<DeleteFilmCommand, DeletedFilmResponse>
    {
        private readonly IFilmRepository _filmRepository;
        private readonly IMapper _mapper;

        public DeleteFilmCommandHandler(IFilmRepository filmRepository, IMapper mapper)
        {
            _filmRepository = filmRepository;
            _mapper = mapper;
        }

        public async Task<DeletedFilmResponse> Handle(DeleteFilmCommand request, CancellationToken cancellationToken)
        {
            Film? film = await _filmRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);

            await _filmRepository.DeleteAsync(film);

            DeletedFilmResponse response = _mapper.Map<DeletedFilmResponse>(film);
            return response;

        }
    }
}
