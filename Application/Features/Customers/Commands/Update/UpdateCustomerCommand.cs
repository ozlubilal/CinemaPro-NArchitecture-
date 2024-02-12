using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customers.Commands.Update;

public class UpdateCustomerCommand:IRequest<UpdatedCustomerResponse> 
{
    public Guid Id { get; set; }
    public int IdentityNumber { get; set; }
    public string FirstName { get; set; }

    public string LastName { get; set; }
    public string PhoneNumber { get; set; }

    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, UpdatedCustomerResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<UpdatedCustomerResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            Customer? customer=await _customerRepository.GetAsync(predicate:c=>c.Id==request.Id);

            customer = _mapper.Map(request,customer);

            await _customerRepository.UpdateAsync(customer);

            UpdatedCustomerResponse response = _mapper.Map<UpdatedCustomerResponse>(customer);

            return response;
        }
    }
}
