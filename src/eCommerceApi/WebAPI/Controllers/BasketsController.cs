using Application.Features.Baskets.Commands.ValidateBasket;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BasketsController : BaseController
{
    [HttpPost("validate")]
    public async Task<ActionResult<ValidateBasketResponse>> Validate([FromBody] ValidateBasketCommand command)
    {
        ValidateBasketResponse response = await Mediator.Send(command);

        return Ok(response);
    }
}
