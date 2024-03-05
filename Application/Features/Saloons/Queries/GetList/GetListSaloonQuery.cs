using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Saloons.Queries.GetList;

public class GetListSaloonQuery:IRequest<GetListResponse<GetListSaloonListItemDto>>
{
    public PageRequest  PageRequest { get; set; }
    public class GetListSaloonQueryHandler:IRequestHandler<GetListSaloonQuery, GetListResponse<GetListSaloonListItemDto>>
    {
        private readonly ISaloonRepository _saloonRepository;
        private readonly IMapper _mapper;

        public GetListSaloonQueryHandler(ISaloonRepository saloonRepository, IMapper mapper)
        {
            _saloonRepository = saloonRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListSaloonListItemDto>> Handle(GetListSaloonQuery request, CancellationToken cancellationToken)
        {
            Paginate<Saloon> saloons = await _saloonRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                withDeleted: false
                );
            GetListResponse<GetListSaloonListItemDto> response=_mapper.Map<GetListResponse<GetListSaloonListItemDto>>(saloons);
            return response;
        }
    }
}
