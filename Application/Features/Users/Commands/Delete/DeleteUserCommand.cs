using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.Delete;

public class DeleteUserCommand:IRequest<DeletedUserResponse> 
{
    public int Id{ get; set; }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeletedUserResponse>
    {
        IUserRepository _userRepository;
        IUserOperationClaimRepository _userOperationClaimRepository;
        IMapper _mapper;

        public DeleteUserCommandHandler(IUserRepository userRepository, IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _userOperationClaimRepository = userOperationClaimRepository;
            _mapper = mapper;
        }

        public async Task<DeletedUserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {

            User? user=await _userRepository.GetAsync(predicate:u=>u.Id==request.Id,cancellationToken:cancellationToken);
            await _userRepository.DeleteAsync(user);

            UserOperationClaim userOperationClaim=await _userOperationClaimRepository.GetAsync(predicate:uc=>uc.UserId==request.Id,cancellationToken:cancellationToken);

            await _userOperationClaimRepository.DeleteAsync(userOperationClaim);
           

            DeletedUserResponse response=_mapper.Map<DeletedUserResponse>(user);

            return response;            
        }
    }
}
