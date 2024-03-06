using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
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

namespace Application.Features.Tickets.Queries.GetListByDynamic;

public class GetListByDynamicTicketQuery: IRequest<GetListResponse<GetListByDynamicTicketListItemDto>>,ISecuredRequest
{
    public string[] Roles => new string[] { "admin", "user" };
    public PageRequest PageRequest { get; set; }
    public DynamicQuery DynamicQuery { get; set; }

    public class GetListByDynamicTicketQueryHandler : IRequestHandler<GetListByDynamicTicketQuery, GetListResponse<GetListByDynamicTicketListItemDto>>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;

        public GetListByDynamicTicketQueryHandler(ITicketRepository ticketRepository, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListByDynamicTicketListItemDto>> Handle(GetListByDynamicTicketQuery request, CancellationToken cancellationToken)
        {
            Paginate<Ticket> tickets = await _ticketRepository.GetListByDynamicAsync(
                request.DynamicQuery,
                include: t => t.Include(t => t.FilmSession).ThenInclude(fs => fs.Film)
                .Include(t => t.FilmSession).ThenInclude(fs => fs.Saloon),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize
            );


            GetListResponse<GetListByDynamicTicketListItemDto> response = _mapper.Map<GetListResponse<GetListByDynamicTicketListItemDto>>(tickets);

            return response;
        }
    }
}
