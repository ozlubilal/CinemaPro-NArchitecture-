using Application.Features.Films.Commands.Create;
using Application.Features.Films.Commands.Delete;
using Application.Features.Films.Commands.Update;
using Application.Features.Films.Queries.GetById;
using Application.Features.Films.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Films.Profiles;

public class MappingProfiles:Profile
{
    public MappingProfiles()
    {
        CreateMap<Film,CreatedFilmResponse>().ReverseMap();
        CreateMap<Film,CreateFilmCommand>().ReverseMap();

        CreateMap<Film, UpdatedFilmResponse>().ReverseMap();
        CreateMap<Film, UpdateFilmCommand>().ReverseMap();

        CreateMap<Film, DeletedFilmResponse>().ReverseMap();
        CreateMap<Film, DeleteFilmCommand>().ReverseMap();

        CreateMap<Film,GetByIdFilmResponse>().ReverseMap();
        CreateMap<Film, GetListFilmListItemDto >().ReverseMap();
        CreateMap<Paginate<Film>,GetListResponse<GetListFilmListItemDto>>().ReverseMap();
    }
}
