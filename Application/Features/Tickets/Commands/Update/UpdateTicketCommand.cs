using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tickets.Commands.Update;

public class UpdateTicketCommand : IRequest<UpdatedTicketResponse>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public Guid FilmSessionId { get; set; }
    public SeatNumber SeatNumber { get; set; }


    public class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand, UpdatedTicketResponse>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;

        public UpdateTicketCommandHandler(ITicketRepository ticketRepository, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        public async Task<UpdatedTicketResponse> Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
        {
            Ticket? ticket = await _ticketRepository.GetAsync(predicate: t => t.Id == request.Id, cancellationToken: cancellationToken);

            ticket = _mapper.Map(request, ticket);

            UpdatedTicketResponse response = _mapper.Map<UpdatedTicketResponse>(ticket);

            return response;
        }
    }
}
