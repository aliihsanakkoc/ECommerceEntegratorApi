using Application.Features.Categories.Commands.Create;
using Application.Features.Categories.Commands.Delete;
using Application.Features.Categories.Commands.Update;
using Application.Features.Categories.Queries.GetById;
using Application.Features.Categories.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Categories.ODataQuery;
using Microsoft.AspNetCore.OData.Query;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedCategoryResponse>> Add([FromBody] CreateCategoryCommand command)
    {
        CreatedCategoryResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedCategoryResponse>> Update([FromBody] UpdateCategoryCommand command)
    {
        UpdatedCategoryResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedCategoryResponse>> Delete([FromRoute] int id)
    {
        DeleteCategoryCommand command = new() { Id = id };

        DeletedCategoryResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdCategoryResponse>> GetById([FromRoute] int id)
    {
        GetByIdCategoryQuery query = new() { Id = id };

        GetByIdCategoryResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListResponse<GetListCategoryListItemDto>>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListCategoryQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListCategoryListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
    [HttpGet("OData")]
    [EnableQuery]
    public async Task<ActionResult<IQueryable<GetListCategoryListItemDto>>> GetList()
    {
        ODataCategoryQuery query = new() ;

        IQueryable<GetListCategoryListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}