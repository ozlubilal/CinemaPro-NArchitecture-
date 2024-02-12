using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Saloons.Commands.Delete;

public class DeleteSaloonCommand:IRequest<DeletedSaloonResponse>
{
    public Guid Id { get; set; }

    public class DeleteSaloonCommandHandler : IRequestHandler<DeleteSaloonCommand, DeletedSaloonResponse>
    {
        private readonly ISaloonRepository _saloonRepository;
        private readonly IMapper _mapper;

        public DeleteSaloonCommandHandler(ISaloonRepository saloonRepository, IMapper mapper)
        {
            _saloonRepository = saloonRepository;
            _mapper = mapper;
        }

        public async Task<DeletedSaloonResponse> Handle(DeleteSaloonCommand request, CancellationToken cancellationToken)
        {
           Saloon? saloon=await _saloonRepository.GetAsync(predicate:s=>s.Id==request.Id,cancellationToken:cancellationToken);
           
            await _saloonRepository.DeleteAsync(saloon);

            DeletedSaloonResponse response=_mapper.Map<DeletedSaloonResponse>(saloon);

            return response;
        }
    }
}
