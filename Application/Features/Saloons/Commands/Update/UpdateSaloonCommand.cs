﻿using Application.Features.Saloons.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Saloons.Commands.Update;

public class UpdateSaloonCommand:IRequest<UpdatedSaloonResponse> ,ISecuredRequest
{
    public string[] Roles => new string[] { "admin" };
    public Guid Id { get; set; }
    public string Name { get; set;}

    public class UpdateSaloonCommandHandler : IRequestHandler<UpdateSaloonCommand, UpdatedSaloonResponse>
    {
        private readonly ISaloonRepository _saloonRepository;
        private readonly IMapper _mapper;
        private readonly SaloonBusinessRules _saloonBusinessRules;

        public UpdateSaloonCommandHandler(ISaloonRepository saloonRepository, IMapper mapper, SaloonBusinessRules saloonBusinessRules)
        {
            _saloonRepository = saloonRepository;
            _mapper = mapper;
            _saloonBusinessRules = saloonBusinessRules;
        }

        public async Task<UpdatedSaloonResponse> Handle(UpdateSaloonCommand request, CancellationToken cancellationToken)
        {
            Saloon? saloon = await _saloonRepository.GetAsync(predicate: s => s.Id == request.Id,cancellationToken:cancellationToken);

            await _saloonBusinessRules.SaloonNameCannotBeDuplicatedWhenInserted(saloon.Name);

            saloon = _mapper.Map(request, saloon);

            await _saloonRepository.UpdateAsync(saloon);

            UpdatedSaloonResponse response = _mapper.Map<UpdatedSaloonResponse>(saloon);

            return response;
        }
    }
}
