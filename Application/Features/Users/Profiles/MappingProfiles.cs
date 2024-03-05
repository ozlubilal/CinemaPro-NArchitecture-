using Application.Features.Tickets.Queries.GetList;
using Application.Features.Users.Commands.Delete;
using Application.Features.Users.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Profiles;

public class MappingProfiles:Profile
{
    public MappingProfiles()
    {
        
        CreateMap<User, GetListUserListItemDto>()
           .ForMember(destinationMember: c => c.Id, memberOptions: opt => opt.MapFrom(c => c.Id))
           .ForMember(destinationMember: c => c.FirstName, memberOptions: opt => opt.MapFrom(c => c.FirstName))
           .ForMember(destinationMember: c => c.LastName, memberOptions: opt => opt.MapFrom(c => c.LastName))
           .ForMember(destinationMember: c => c.Email, memberOptions: opt => opt.MapFrom(c => c.Email))
           .ForMember(destinationMember: c => c.OperationClaimName, memberOptions: opt => opt.MapFrom(c => c.UserOperationClaims.FirstOrDefault().OperationClaim.Name))
           .ReverseMap();
        CreateMap<Paginate<User>, GetListResponse<GetListUserListItemDto>>().ReverseMap();

        CreateMap<User,DeletedUserResponse>().ReverseMap();
    }
}
