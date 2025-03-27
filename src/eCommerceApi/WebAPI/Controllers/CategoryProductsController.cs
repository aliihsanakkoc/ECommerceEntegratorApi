using Application.Features.CategoryProducts.Commands.Create;
using Application.Features.CategoryProducts.Commands.Delete;
using Application.Features.CategoryProducts.Commands.Update;
using Application.Features.CategoryProducts.Queries.GetById;
using Application.Features.CategoryProducts.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Features.CategoryProducts.RangeCommands;
using Microsoft.AspNetCore.OData.Query;
using Application.Features.CategoryProducts.ODataQuery;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryProductsController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedCategoryProductResponse>> Add([FromBody] CreateCategoryProductCommand command)
    {
        CreatedCategoryProductResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPost("AddRange")]
    public async Task<ActionResult<Unit>> AddRange([FromBody] AddRangeCategoryProductsCommand command)
    {
        Unit response = await Mediator.Send(command);

        return Ok(response);
    }
    [HttpPost("DeleteRange")]
    public async Task<ActionResult<Unit>> DeleteRange([FromBody] DeleteRangeCategoryProductsCommand command)
    {
        Unit response = await Mediator.Send(command);

        return Ok(response);
    }
    [HttpPut]
    public async Task<ActionResult<UpdatedCategoryProductResponse>> Update([FromBody] UpdateCategoryProductCommand command)
    {
        UpdatedCategoryProductResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedCategoryProductResponse>> Delete([FromRoute] int id)
    {
        DeleteCategoryProductCommand command = new() { Id = id };

        DeletedCategoryProductResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdCategoryProductResponse>> GetById([FromRoute] int id)
    {
        GetByIdCategoryProductQuery query = new() { Id = id };

        GetByIdCategoryProductResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListResponse<GetListCategoryProductListItemDto>>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListCategoryProductQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListCategoryProductListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
    [HttpGet("OData")]
    [EnableQuery]   
    public async Task<ActionResult<IQueryable<GetListCategoryProductListItemDto>>> GetList()
    {
        ODataCategoryProductQuery query = new() ;

        IQueryable<GetListCategoryProductListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}