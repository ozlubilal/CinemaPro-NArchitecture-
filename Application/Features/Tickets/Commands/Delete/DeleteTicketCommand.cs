using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tickets.Commands.Delete;

public class DeleteTicketCommand:IRequest<DeletedTicketResponse>
{
    public  Guid Id { get; set; }

    public class DeleteTicketCommandHandler : IRequestHandler<DeleteTicketCommand, DeletedTicketResponse>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;

        public DeleteTicketCommandHandler(ITicketRepository ticketRepository, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        public async Task<DeletedTicketResponse> Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
        {
            Ticket? ticket = await _ticketRepository.GetAsync(predicate: t => t.Id == request.Id, cancellationToken: cancellationToken);

            await _ticketRepository.DeleteAsync(ticket);

            DeletedTicketResponse response = _mapper.Map<DeletedTicketResponse>(ticket);

            return response;

               
        }
    }
}
