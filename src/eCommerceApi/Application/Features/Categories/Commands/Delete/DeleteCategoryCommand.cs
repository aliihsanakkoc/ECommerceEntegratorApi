using Application.Features.Categories.Constants;
using Application.Features.Categories.Constants;
using Application.Features.Categories.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Categories.Constants.CategoriesOperationClaims;

namespace Application.Features.Categories.Commands.Delete;

public class DeleteCategoryCommand : IRequest<DeletedCategoryResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Write, CategoriesOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetCategories"];

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, DeletedCategoryResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly CategoryBusinessRules _categoryBusinessRules;

        public DeleteCategoryCommandHandler(IMapper mapper, ICategoryRepository categoryRepository,
                                         CategoryBusinessRules categoryBusinessRules)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _categoryBusinessRules = categoryBusinessRules;
        }

        public async Task<DeletedCategoryResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            Category? category = await _categoryRepository.GetAsync(predicate: c => c.Id == request.Id, cancellationToken: cancellationToken);
            await _categoryBusinessRules.CategoryShouldExistWhenSelected(category);
            await _categoryBusinessRules.CategoryShouldNotTopCategory(request.Id, cancellationToken);  
            await _categoryRepository.DeleteAsync(category!);

            DeletedCategoryResponse response = _mapper.Map<DeletedCategoryResponse>(category);
            return response;
        }
    }
}