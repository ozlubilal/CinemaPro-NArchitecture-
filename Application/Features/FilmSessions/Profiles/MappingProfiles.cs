
using Application.Features.FilmSessions.Commands.Create;
using Application.Features.FilmSessions.Commands.Delete;
using Application.Features.FilmSessions.Commands.Update;
using Application.Features.FilmSessions.Queries.GetById;
using Application.Features.FilmSessions.Queries.GetList;
using Application.Features.FilmSessions.Queries.GetListByDynamic;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FilmSessions.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<FilmSession, CreatedFilmSessionResponse>().ReverseMap();
        CreateMap<FilmSession, CreateFilmSessionCommand>().ReverseMap();

        CreateMap<FilmSession, UpdatedFilmSessionResponse>().ReverseMap();
        CreateMap<FilmSession, UpdateFilmSessionCommand>().ReverseMap();

        CreateMap<FilmSession, DeletedFilmSessionResponse>().ReverseMap();
        CreateMap<FilmSession, DeleteFilmSessionCommand>().ReverseMap();




        CreateMap<UpdateFilmSessionCommand, GetByIdFilmSessionResponse>().ReverseMap();

        CreateMap<FilmSession, GetByIdFilmSessionResponse>()
            .ForMember(destinationMember: c => c.SaloonName, memberOptions: opt => opt.MapFrom(c => c.Saloon.Name))
            .ForMember(destinationMember: c => c.ImageUrl, memberOptions: opt => opt.MapFrom(c => c.Film.ImageUrl))
            .ReverseMap();
        CreateMap<FilmSession, GetListFilmSessionListItemDto>()
            .ForMember(destinationMember: c => c.SaloonName, memberOptions: opt => opt.MapFrom(c => c.Saloon.Name))
            .ForMember(destinationMember: c => c.ImageUrl, memberOptions: opt => opt.MapFrom(c => c.Film.ImageUrl))
            .ReverseMap();
        CreateMap<FilmSession, GetListByDynamicFilmSessionListItemDto>()
              .ForMember(destinationMember: c => c.SaloonName, memberOptions: opt => opt.MapFrom(c => c.Saloon.Name))
            .ForMember(destinationMember: c => c.ImageUrl, memberOptions: opt => opt.MapFrom(c => c.Film.ImageUrl))
            .ReverseMap();
        CreateMap<Paginate<FilmSession>, GetListResponse<GetListFilmSessionListItemDto>>().ReverseMap();
        CreateMap<Paginate<FilmSession>, GetListResponse<GetListByDynamicFilmSessionListItemDto>>().ReverseMap();
    }
}
