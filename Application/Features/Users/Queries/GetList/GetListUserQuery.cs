using Application.Features.Saloons.Queries.GetList;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries.GetList;

public class GetListUserQuery:IRequest<GetListResponse<GetListUserListItemDto>>,ISecuredRequest
{
    public string[] Roles => new string[] { "admin"};
    public PageRequest PageRequest { get; set; }
    public class GetListUserQueryHanlder : IRequestHandler<GetListUserQuery, GetListResponse<GetListUserListItemDto>>
    {
        IUserRepository _userRepository;
        IMapper _mapper;

        public GetListUserQueryHanlder(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListUserListItemDto>> Handle(GetListUserQuery request, CancellationToken cancellationToken)
        {
            Paginate<User>? users =await _userRepository.GetListAsync(
               index:request.PageRequest.PageIndex,
               size:request.PageRequest.PageSize,
               cancellationToken:cancellationToken,
               withDeleted:true,
               include:u=>u.Include(u=>u.UserOperationClaims).ThenInclude(uc => uc.OperationClaim)
                );

            GetListResponse<GetListUserListItemDto> response = _mapper.Map<GetListResponse<GetListUserListItemDto>>(users);

            return response;
        }
    }
}
