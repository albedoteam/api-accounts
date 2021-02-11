using AlbedoTeam.Sdk.FailFast;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    public abstract class BaseAlbedoController : ControllerBase
    {
        protected readonly IMediator _mediator;

        public BaseAlbedoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected virtual ActionResult HandleError<T>(Result<T> response)
        {
            ObjectResult DefaultError()
            {
                return Problem(string.Join(", ", response.Errors));
            }

            return response.FailureReason switch
            {
                FailureReason.Conflict => Conflict(response.Errors),
                FailureReason.BadRequest => BadRequest(response.Errors),
                FailureReason.NotFound => NotFound(response.Errors),
                FailureReason.InternalServerError => DefaultError(),
                null => DefaultError(),
                _ => DefaultError()
            };
        }
    }
}