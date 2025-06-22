using DotNet.CleanArch.Template.Application.Features.Authentication.Commands;
using DotNet.CleanArch.Template.WebApi.Helpers;
using DotNet.CleanArch.Template.WebApi.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DotNet.CleanArch.Template.WebApi.Controllers
{
    [ApiController]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)] 
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status401Unauthorized)] 
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status403Forbidden)] 
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticateCommand command)
        {
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }
    }
}
