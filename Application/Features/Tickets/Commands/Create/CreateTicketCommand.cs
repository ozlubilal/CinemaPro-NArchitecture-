using Application.Features.Tickets.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tickets.Commands.Create;

public class CreateTicketCommand : IRequest<CreatedTicketResponse>,ISecuredRequest
{
    public string[] Roles => new string[] { "admin" };
    public string? FirstName { get; set; }
    public string?  LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public Guid FilmSessionId { get; set; }
    public decimal Price { get; set; }
    public List<string> SelectedSeats { get; set; }


    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, CreatedTicketResponse>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;
        private readonly TicketBusinessRules _ticketBusinessRules;

        public CreateTicketCommandHandler(ITicketRepository ticketRepository, IMapper mapper, TicketBusinessRules ticketBusinessRules)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
            _ticketBusinessRules = ticketBusinessRules;
        }

        public async Task<CreatedTicketResponse> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {

           await _ticketBusinessRules.SeatNumberCannotDuplicatedForFilmSession(request.SelectedSeats,request.FilmSessionId);

            Customer customer=await _ticketBusinessRules.CustomerIsExist(request.FirstName,request.LastName,request.PhoneNumber);

            Ticket ticket = _mapper.Map<Ticket>(request);

            await _ticketRepository.AddAsync(ticket);

            CreatedTicketResponse response = _mapper.Map<CreatedTicketResponse>(ticket);

            return response;
        }
    }
}
