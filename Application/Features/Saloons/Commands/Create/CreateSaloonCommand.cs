using Application.Features.Saloons.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Saloons.Commands.Create;

public class CreateSaloonCommand:IRequest<CreatedSaloonResponse>
{
    public string Name { get; set; }

    public class CreateSaloonCommandHandler : IRequestHandler<CreateSaloonCommand, CreatedSaloonResponse>
    {
        private readonly ISaloonRepository _saloonRepository;
        private readonly IMapper _mapper;
        private readonly SaloonBusinessRules _saloonBusinessRules;

        public CreateSaloonCommandHandler(ISaloonRepository saloonRepository, IMapper mapper, SaloonBusinessRules saloonBusinessRules
            )
        {
            _saloonRepository = saloonRepository;
            _mapper = mapper;
            _saloonBusinessRules = saloonBusinessRules;
        }

        public async Task<CreatedSaloonResponse> Handle(CreateSaloonCommand request, CancellationToken cancellationToken)
        {
            Saloon saloon=_mapper.Map<Saloon>(request);

            await _saloonBusinessRules.SaloonNameCannotBeDuplicatedWhenInserted(saloon.Name);

            await _saloonRepository.AddAsync(saloon);

            CreatedSaloonResponse createdSaloonResponse = _mapper.Map<CreatedSaloonResponse>(saloon);
            
            return createdSaloonResponse;
        }
    }
}
