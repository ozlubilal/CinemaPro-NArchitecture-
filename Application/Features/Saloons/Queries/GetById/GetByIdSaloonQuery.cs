using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Saloons.Queries.GetById;

public class GetByIdSaloonQuery : IRequest<GetByIdSaloonReponse>
{
    public Guid Id { get; set; }
    public class GetByIdSaloonQueryHandle : IRequestHandler<GetByIdSaloonQuery, GetByIdSaloonReponse>
    {
        private readonly ISaloonRepository _saloonRepository;
        private readonly IMapper _mapper;

        public GetByIdSaloonQueryHandle(ISaloonRepository saloonRepository, IMapper mapper)
        {
            _saloonRepository = saloonRepository;
            _mapper = mapper;
        }

        public async Task<GetByIdSaloonReponse> Handle(GetByIdSaloonQuery request, CancellationToken cancellationToken)
        {
            Saloon? saloon = await _saloonRepository.GetAsync(predicate: s => s.Id == request.Id);

            GetByIdSaloonReponse response = _mapper.Map<GetByIdSaloonReponse>(saloon);

            return response;
        }
    }

}
