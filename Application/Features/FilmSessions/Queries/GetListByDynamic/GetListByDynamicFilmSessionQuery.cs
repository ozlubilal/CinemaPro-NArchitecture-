using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FilmSessions.Queries.GetListByDynamic;

public class GetListByDynamicFilmSessionQuery:IRequest<GetListResponse<GetListByDynamicFilmSessionListItemDto>>
{
    
    public PageRequest  PageRequest { get; set; }
    public DynamicQuery DynamicQuery { get; set; }
    public class GetListByDynamicFilmSessionQueryHandler : IRequestHandler<GetListByDynamicFilmSessionQuery, GetListResponse<GetListByDynamicFilmSessionListItemDto>>
    {
        private readonly IFilmSessionRepository _filmSessionRepository;
        private readonly IMapper _mapper;

        public GetListByDynamicFilmSessionQueryHandler(IFilmSessionRepository filmSessionRepository, IMapper mapper)
        {
            _filmSessionRepository = filmSessionRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListByDynamicFilmSessionListItemDto>> Handle(GetListByDynamicFilmSessionQuery request, CancellationToken cancellationToken)
        {
            Paginate<FilmSession> filmSessions = await _filmSessionRepository.GetListByDynamicAsync(
                request.DynamicQuery,
                include: f => f.Include(f => f.Film).Include(f => f.Saloon),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize);
            GetListResponse<GetListByDynamicFilmSessionListItemDto> response = _mapper.Map<GetListResponse<GetListByDynamicFilmSessionListItemDto>>(filmSessions);
            return response;
        }
    }
}
