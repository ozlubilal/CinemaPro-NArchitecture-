using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Responses;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Films.Queries.GetById;

public class GetByIdFilmQuery : IRequest<GetByIdFilmResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles =>new string[] {"admin","user"};

    public  class GetByIdFilmQueryHandler : IRequestHandler<GetByIdFilmQuery, GetByIdFilmResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFilmRepository _brandRepository;

        public GetByIdFilmQueryHandler(IMapper mapper, IFilmRepository brandRepository)
        {
            _mapper = mapper;
            _brandRepository = brandRepository;
        }

        public async Task<GetByIdFilmResponse> Handle(GetByIdFilmQuery request, CancellationToken cancellationToken)
        {
            Film? film=await _brandRepository.GetAsync(predicate:f=>f.Id == request.Id,withDeleted:true ,cancellationToken:cancellationToken);

            GetByIdFilmResponse response= _mapper.Map<GetByIdFilmResponse>(film);

            return response;
        }
    }
}
