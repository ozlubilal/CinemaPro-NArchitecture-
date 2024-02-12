﻿using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Core.Persistence.Repositories;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Films.Queries.GetList;

public class GetListFilmQuery:IRequest<GetListResponse<GetListFilmListItemDto>>
{
    public PageRequest  PageRequest  { get; set; }

    public class GetListFilmQueryHandler : IRequestHandler<GetListFilmQuery, GetListResponse<GetListFilmListItemDto>>
    {
        private readonly IFilmRepository _filmRepository;
        private readonly IMapper _mapper;

        public GetListFilmQueryHandler(IFilmRepository filmRepository, IMapper mapper)
        {
            _filmRepository = filmRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListFilmListItemDto>> Handle(GetListFilmQuery request, CancellationToken cancellationToken)
        {
            Paginate<Film> films = await _filmRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                withDeleted: true
                );
            GetListResponse<GetListFilmListItemDto> response = _mapper.Map<GetListResponse<GetListFilmListItemDto>>(films);
            return response;
        }
    }
}