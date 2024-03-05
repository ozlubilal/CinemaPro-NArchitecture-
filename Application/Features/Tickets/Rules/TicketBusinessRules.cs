using Application.Features.Tickets.Constants;
using Application.Services.Repositories;
using Core.Application.Responses;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tickets.Rules;

public class TicketBusinessRules : BaseBusinessRules
{
    private readonly ITicketRepository _ticketRepository;
    private readonly ICustomerRepository _customerRepository;

    public TicketBusinessRules(ITicketRepository ticketRepository, ICustomerRepository customerRepository)
    {
        _ticketRepository = ticketRepository;
        _customerRepository = customerRepository;
    }

    public async Task SeatNumberCannotDuplicatedForFilmSession(List<string> selectedSeats, Guid filmSessionId)
    {
        IList<Ticket?> result = (await _ticketRepository.GetListAsync(predicate: t => t.FilmSessionId == filmSessionId)).Items;

        if (result != null)
        {
            var allSelectedseats = result.SelectMany(ticket => ticket.SelectedSeats).ToList();

            var duplicatedSeat = selectedSeats.Any(seat => allSelectedseats.Any(seat2=>seat2.Contains(seat)));

            if (duplicatedSeat)
            {
                throw new BusinessException(TicketMessages.TicketOfSeatNumberExist);
            }
        }
    }
    public async Task<Customer> CustomerIsExist(string firstName, string lastName, string phoneNumber)
    {
        //customer varsa mevcut customer döndürüyoruz, yoksa yeni bir customer oluşturup onu döndürüyoruz
        Customer? customer = await _customerRepository.GetAsync(predicate: c => c.FirstName == firstName && c.LastName == lastName && c.PhoneNumber == phoneNumber);

        if (customer != null)
        {
            return customer;
        }
        else
        {
            Customer customer1 = new()
            {
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber
            };

            await _customerRepository.AddAsync(customer1);
            return customer1;
        }

    }

}
