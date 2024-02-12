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

        public CreateSaloonCommandHandler(ISaloonRepository saloonRepository, IMapper mapper)
        {
            _saloonRepository = saloonRepository;
            _mapper = mapper;
        }

        public async Task<CreatedSaloonResponse> Handle(CreateSaloonCommand request, CancellationToken cancellationToken)
        {
            Saloon saloon=_mapper.Map<Saloon>(request);
            
            await _saloonRepository.AddAsync(saloon);

            CreatedSaloonResponse createdSaloonResponse = _mapper.Map<CreatedSaloonResponse>(saloon);
            
            return createdSaloonResponse;
        }
    }
}
