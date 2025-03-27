using Application.Features.VariantProducts.Commands.Create;
using Application.Features.VariantProducts.Commands.Delete;
using Application.Features.VariantProducts.Commands.Update;
using Application.Features.VariantProducts.Queries.GetById;
using Application.Features.VariantProducts.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.VariantProducts.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateVariantProductCommand, VariantProduct>();
        CreateMap<VariantProduct, CreatedVariantProductResponse>();

        CreateMap<UpdateVariantProductCommand, VariantProduct>();
        CreateMap<VariantProduct, UpdatedVariantProductResponse>();

        CreateMap<DeleteVariantProductCommand, VariantProduct>();
        CreateMap<VariantProduct, DeletedVariantProductResponse>();

        CreateMap<VariantProduct, GetByIdVariantProductResponse>();

        CreateMap<VariantProduct, GetListVariantProductListItemDto>();
        CreateMap<IPaginate<VariantProduct>, GetListResponse<GetListVariantProductListItemDto>>();
    }
}