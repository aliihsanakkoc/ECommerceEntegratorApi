using Application.Features.Clothings.Commands.Create;
using Application.Features.Clothings.Commands.Delete;
using Application.Features.Clothings.Commands.Update;
using Application.Features.Clothings.Queries.GetById;
using Application.Features.Clothings.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Application.Features.Clothings.ODataQuery;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClothingsController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedClothingResponse>> Add([FromBody] CreateClothingCommand command)
    {
        CreatedClothingResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedClothingResponse>> Update([FromBody] UpdateClothingCommand command)
    {
        UpdatedClothingResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedClothingResponse>> Delete([FromRoute] int id)
    {
        DeleteClothingCommand command = new() { Id = id };

        DeletedClothingResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdClothingResponse>> GetById([FromRoute] int id)
    {
        GetByIdClothingQuery query = new() { Id = id };

        GetByIdClothingResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListResponse<GetListClothingListItemDto>>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListClothingQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListClothingListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
    [HttpGet("OData")]
    [EnableQuery]
    public async Task<ActionResult<IQueryable<GetListClothingListItemDto>>> GetList()
    {
        ODataClothingQuery query = new();

        IQueryable<GetListClothingListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}