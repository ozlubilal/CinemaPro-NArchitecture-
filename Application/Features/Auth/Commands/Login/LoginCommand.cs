using Application.Features.Auth.Dtos;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.Jwt;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Login;

public class LoginCommand : IRequest<LoginedDto>
{
    public UserForLoginDto UserForLoginDto { get; set; }
    public string IpAddress { get; set; }


    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginedDto>
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;

        public LoginCommandHandler(IAuthService authService, IUserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        public async Task<LoginedDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var userToCheck = await _userRepository.GetAsync(predicate: u => u.Email == request.UserForLoginDto.Email,
                include:u=>u.Include(u=>u.UserOperationClaims));
            if (userToCheck == null)
            {
                throw new Exception("Kullanıcı Bulunamadı");
            }
            if (!HashingHelper.VerifyPasswordHash(request.UserForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                throw new Exception("Parola hatalı");
            }

            AccessToken createdAccessToken = await _authService.CreateAccessToken(userToCheck);
            RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(userToCheck, request.IpAddress);
            RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

            LoginedDto loginedDto = new()
            {
               OperationClaimId=userToCheck.UserOperationClaims.FirstOrDefault().OperationClaimId,
                RefreshToken = addedRefreshToken,
                AccessToken = createdAccessToken,
            };

            return loginedDto;

        }
    }
}
