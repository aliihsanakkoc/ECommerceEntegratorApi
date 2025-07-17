using Application.Features.Foods.Commands.Create;
using Application.Features.Foods.Commands.Delete;
using Application.Features.Foods.Commands.Update;
using Application.Features.Foods.Queries.GetById;
using Application.Features.Foods.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Application.Features.Foods.ODataQuery;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FoodsController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedFoodResponse>> Add([FromBody] CreateFoodCommand command)
    {
        CreatedFoodResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedFoodResponse>> Update([FromBody] UpdateFoodCommand command)
    {
        UpdatedFoodResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedFoodResponse>> Delete([FromRoute] int id)
    {
        DeleteFoodCommand command = new() { Id = id };

        DeletedFoodResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdFoodResponse>> GetById([FromRoute] int id)
    {
        GetByIdFoodQuery query = new() { Id = id };

        GetByIdFoodResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListResponse<GetListFoodListItemDto>>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListFoodQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListFoodListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
    [HttpGet("OData")]
    [EnableQuery]
    public async Task<ActionResult<IQueryable<GetListFoodListItemDto>>> GetList()
    {
        ODataFoodQuery query = new();

        IQueryable<GetListFoodListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}