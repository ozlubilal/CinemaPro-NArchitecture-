﻿using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Rules;

public class AuthBusinessRules
{
    private readonly IUserRepository _userRepository;

    public AuthBusinessRules(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task EmailCanNotBeDuplicatedWhenRegistered(string email)
    {
        User? user = await _userRepository.GetAsync(u => u.Email == email);
        if (user != null) throw new BusinessException("Mail already exists");

    }
}
