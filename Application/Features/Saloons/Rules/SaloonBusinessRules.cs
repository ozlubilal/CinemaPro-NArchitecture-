using Application.Features.Saloons.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Saloons.Rules;

public class SaloonBusinessRules:BaseBusinessRules
{
    private readonly ISaloonRepository _saloonRepository;

    public SaloonBusinessRules(ISaloonRepository saloonRepository)
    {
        _saloonRepository = saloonRepository;
    }

    public async Task SaloonNameCannotBeDuplicatedWhenInserted(string name)
    {
        Saloon?  result=await _saloonRepository.GetAsync(predicate: s=>s.Name == name);

        if (result != null)
        {
            throw new BusinessException(SaloonMessages.SaloonNameExist);
        }

    }
}
