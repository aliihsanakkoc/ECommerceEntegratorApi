using Application.Features.Clothings.Commands.Create;
using Application.Features.Clothings.Commands.Delete;
using Application.Features.Clothings.Commands.Update;
using Application.Features.Clothings.Queries.GetById;
using Application.Features.Clothings.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Clothings.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateClothingCommand, Clothing>();
        CreateMap<Clothing, CreatedClothingResponse>();

        CreateMap<UpdateClothingCommand, Clothing>();
        CreateMap<Clothing, UpdatedClothingResponse>();

        CreateMap<DeleteClothingCommand, Clothing>();
        CreateMap<Clothing, DeletedClothingResponse>();

        CreateMap<Clothing, GetByIdClothingResponse>();

        CreateMap<Clothing, GetListClothingListItemDto>();
        CreateMap<IPaginate<Clothing>, GetListResponse<GetListClothingListItemDto>>();
    }
}