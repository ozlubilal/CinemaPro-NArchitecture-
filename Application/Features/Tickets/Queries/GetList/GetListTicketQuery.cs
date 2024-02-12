using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tickets.Queries.GetList;

public class GetListTicketQuery:IRequest<GetListResponse<GetListTicketListItemDto>>
{
    public PageRequest PageRequest { get; set; }
    public class GetListTicketCommandHandler : IRequestHandler<GetListTicketQuery, GetListResponse<GetListTicketListItemDto>>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;

        public GetListTicketCommandHandler(ITicketRepository ticketRepository, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListTicketListItemDto>> Handle(GetListTicketQuery request, CancellationToken cancellationToken)
        {
            Paginate<Ticket> tickets =await  _ticketRepository.GetListAsync(
                include:t=>t.Include(t=>t.FilmSession).ThenInclude(fs => fs.Film).
                Include(t => t.FilmSession).ThenInclude(fs => fs.Saloon).
                Include(t=>t.Customer),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize
                );

            GetListResponse<GetListTicketListItemDto> response =_mapper.Map<GetListResponse<GetListTicketListItemDto>>(tickets);
            
            return response;


        }
    }
}
