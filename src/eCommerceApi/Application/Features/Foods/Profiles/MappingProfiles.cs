using Application.Features.Foods.Commands.Create;
using Application.Features.Foods.Commands.Delete;
using Application.Features.Foods.Commands.Update;
using Application.Features.Foods.Queries.GetById;
using Application.Features.Foods.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Foods.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateFoodCommand, Food>();
        CreateMap<Food, CreatedFoodResponse>();

        CreateMap<UpdateFoodCommand, Food>();
        CreateMap<Food, UpdatedFoodResponse>();

        CreateMap<DeleteFoodCommand, Food>();
        CreateMap<Food, DeletedFoodResponse>();

        CreateMap<Food, GetByIdFoodResponse>();

        CreateMap<Food, GetListFoodListItemDto>();
        CreateMap<IPaginate<Food>, GetListResponse<GetListFoodListItemDto>>();
    }
}