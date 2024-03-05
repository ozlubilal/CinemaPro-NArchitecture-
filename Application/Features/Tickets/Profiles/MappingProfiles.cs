using Application.Features.Tickets.Commands.Create;
using Application.Features.Tickets.Commands.Delete;
using Application.Features.Tickets.Commands.Update;
using Application.Features.Tickets.Queries.GetList;
using Application.Features.Tickets.Queries.GetListByDynamic;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tickets.Profiles
{
    internal class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Ticket, CreatedTicketResponse>().ReverseMap();
            CreateMap<Ticket, CreateTicketCommand>().ReverseMap();

            CreateMap<Ticket, UpdatedTicketResponse>().ReverseMap();
            CreateMap<Ticket, UpdateTicketCommand>().ReverseMap();

            CreateMap<Ticket, DeletedTicketResponse>().ReverseMap();
            CreateMap<Ticket, DeleteTicketCommand>().ReverseMap();

            // CreateMap<Ticket, GetByIdTicketResponse>().ReverseMap();
            CreateMap<Ticket, GetListTicketListItemDto>()
            .ForMember(destinationMember: c => c.FilmName, memberOptions: opt => opt.MapFrom(c => c.FilmSession.Film.Name))
            .ForMember(destinationMember: c => c.SaloonName, memberOptions: opt => opt.MapFrom(c => c.FilmSession.Saloon.Name))
            .ForMember(destinationMember: c => c.Price, memberOptions: opt => opt.MapFrom(c => c.FilmSession.Price))
            .ForMember(destinationMember:c=>c.FilmSessionDate, memberOptions:opt=>opt.MapFrom(c=>c.FilmSession.FilmSessionDate))
            .ForMember(destinationMember:c=>c.StartTime, memberOptions:opt=>opt.MapFrom(c=>c.FilmSession.StartTime))
            .ReverseMap();

            CreateMap<Ticket, GetListByDynamicTicketListItemDto>()
           .ForMember(destinationMember: c => c.FilmName, memberOptions: opt => opt.MapFrom(c => c.FilmSession.Film.Name))
           .ForMember(destinationMember: c => c.SaloonName, memberOptions: opt => opt.MapFrom(c => c.FilmSession.Saloon.Name))
           .ForMember(destinationMember: c => c.Price, memberOptions: opt => opt.MapFrom(c => c.FilmSession.Price))
            .ForMember(destinationMember: c => c.FilmSessionDate, memberOptions: opt => opt.MapFrom(c => c.FilmSession.FilmSessionDate))
            .ForMember(destinationMember: c => c.StartTime, memberOptions: opt => opt.MapFrom(c => c.FilmSession.StartTime))
           .ReverseMap();

            CreateMap<Paginate<Ticket>, GetListResponse<GetListTicketListItemDto>>().ReverseMap();
            CreateMap<Paginate<Ticket>, GetListResponse<GetListByDynamicTicketListItemDto>>().ReverseMap();
        }
    }
}
