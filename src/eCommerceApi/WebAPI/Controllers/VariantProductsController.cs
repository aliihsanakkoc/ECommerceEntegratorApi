using Application.Features.VariantProducts.Commands.Create;
using Application.Features.VariantProducts.Commands.Delete;
using Application.Features.VariantProducts.Commands.Update;
using Application.Features.VariantProducts.ODataQuery;
using Application.Features.VariantProducts.Queries.GetById;
using Application.Features.VariantProducts.Queries.GetList;
using Application.Features.VariantProducts.RangeCommands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VariantProductsController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedVariantProductResponse>> Add([FromBody] CreateVariantProductCommand command)
    {
        CreatedVariantProductResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPost("AddRange")]
    public async Task<ActionResult<Unit>> AddRange([FromBody] AddRangeVariantProductsCommand command)
    {
        Unit response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpPost("DeleteRange")]
    public async Task<ActionResult<Unit>> DeleteRange([FromBody] DeleteRangeVariantProductsCommand command)
    {
        Unit response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedVariantProductResponse>> Update([FromBody] UpdateVariantProductCommand command)
    {
        UpdatedVariantProductResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedVariantProductResponse>> Delete([FromRoute] int id)
    {
        DeleteVariantProductCommand command = new() { Id = id };

        DeletedVariantProductResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdVariantProductResponse>> GetById([FromRoute] int id)
    {
        GetByIdVariantProductQuery query = new() { Id = id };

        GetByIdVariantProductResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListResponse<GetListVariantProductListItemDto>>> GetList(
        [FromQuery] PageRequest pageRequest
    )
    {
        GetListVariantProductQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListVariantProductListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet("OData")]
    [EnableQuery]
    public async Task<ActionResult<IQueryable<GetListVariantProductListItemDto>>> GetList()
    {
        ODataVariantProductQuery query = new();

        IQueryable<GetListVariantProductListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}
