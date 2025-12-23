using Application.Features.Variants.Commands.Create;
using Application.Features.Variants.Commands.Delete;
using Application.Features.Variants.Commands.Update;
using Application.Features.Variants.ODataQuery;
using Application.Features.Variants.Queries.GetById;
using Application.Features.Variants.Queries.GetList;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VariantsController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedVariantResponse>> Add([FromBody] CreateVariantCommand command)
    {
        CreatedVariantResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedVariantResponse>> Update([FromBody] UpdateVariantCommand command)
    {
        UpdatedVariantResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedVariantResponse>> Delete([FromRoute] int id)
    {
        DeleteVariantCommand command = new() { Id = id };

        DeletedVariantResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdVariantResponse>> GetById([FromRoute] int id)
    {
        GetByIdVariantQuery query = new() { Id = id };

        GetByIdVariantResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListResponse<GetListVariantListItemDto>>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListVariantQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListVariantListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet("OData")]
    [EnableQuery]
    public async Task<ActionResult<IQueryable<GetListVariantListItemDto>>> GetList()
    {
        ODataVariantQuery query = new();

        IQueryable<GetListVariantListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}
