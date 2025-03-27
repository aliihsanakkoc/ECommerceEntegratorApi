using Domain.Abstracts;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using System.Reflection;
using static Application.Features.Products.Constants.ProductsOperationClaims;

namespace Application.Features.Products.ProductType;
public class ProductTypeQuery : IRequest<List<string>>, ISecuredRequest
{
    public string[] Roles => [Admin,Read];

    public class ProductTypeQueryHandler : IRequestHandler<ProductTypeQuery, List<string>>
    {
        public Task<List<string>> Handle(ProductTypeQuery request, CancellationToken cancellationToken)
        {
            List<Type> productTypes = Assembly.Load("Domain").GetTypes()
                .Where(t => typeof(IProduct).IsAssignableFrom(t) && t.IsClass)
                .ToList();
            return Task.FromResult(productTypes.Select(t => t.Name).ToList());
        }
    }
}
