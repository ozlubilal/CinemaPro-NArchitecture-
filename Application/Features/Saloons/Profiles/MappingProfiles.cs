using Application.Features.Saloons.Commands.Create;
using Application.Features.Saloons.Commands.Delete;
using Application.Features.Saloons.Commands.Update;
using Application.Features.Saloons.Queries.GetById;
using Application.Features.Saloons.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Saloons.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Saloon, CreateSaloonCommand>().ReverseMap();
        CreateMap<Saloon, CreatedSaloonResponse>().ReverseMap();

        CreateMap<Saloon, DeleteSaloonCommand>().ReverseMap();
        CreateMap<Saloon, DeletedSaloonResponse>().ReverseMap();

        CreateMap<Saloon, UpdateSaloonCommand>().ReverseMap();
        CreateMap<Saloon, UpdatedSaloonResponse>().ReverseMap();

        CreateMap<Saloon,GetListSaloonListItemDto>().ReverseMap();
        CreateMap<Saloon, GetByIdSaloonReponse>().ReverseMap();
        CreateMap<Paginate<Saloon>,GetListResponse<GetListSaloonListItemDto>>().ReverseMap();
    
    }
}

