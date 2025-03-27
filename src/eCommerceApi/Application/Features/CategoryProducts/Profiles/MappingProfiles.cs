using Application.Features.CategoryProducts.Commands.Create;
using Application.Features.CategoryProducts.Commands.Delete;
using Application.Features.CategoryProducts.Commands.Update;
using Application.Features.CategoryProducts.Queries.GetById;
using Application.Features.CategoryProducts.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.CategoryProducts.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateCategoryProductCommand, CategoryProduct>();
        CreateMap<CategoryProduct, CreatedCategoryProductResponse>();

        CreateMap<UpdateCategoryProductCommand, CategoryProduct>();
        CreateMap<CategoryProduct, UpdatedCategoryProductResponse>();

        CreateMap<DeleteCategoryProductCommand, CategoryProduct>();
        CreateMap<CategoryProduct, DeletedCategoryProductResponse>();

        CreateMap<CategoryProduct, GetByIdCategoryProductResponse>();

        CreateMap<CategoryProduct, GetListCategoryProductListItemDto>();
        CreateMap<IPaginate<CategoryProduct>, GetListResponse<GetListCategoryProductListItemDto>>();
    }
}