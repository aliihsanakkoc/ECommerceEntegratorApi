using Application.Features.Variants.Commands.Create;
using Application.Features.Variants.Commands.Delete;
using Application.Features.Variants.Commands.Update;
using Application.Features.Variants.Queries.GetById;
using Application.Features.Variants.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Variants.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateVariantCommand, Variant>();
        CreateMap<Variant, CreatedVariantResponse>();

        CreateMap<UpdateVariantCommand, Variant>();
        CreateMap<Variant, UpdatedVariantResponse>();

        CreateMap<DeleteVariantCommand, Variant>();
        CreateMap<Variant, DeletedVariantResponse>();

        CreateMap<Variant, GetByIdVariantResponse>();

        CreateMap<Variant, GetListVariantListItemDto>();
        CreateMap<IPaginate<Variant>, GetListResponse<GetListVariantListItemDto>>();
    }
}