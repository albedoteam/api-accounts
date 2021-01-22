using System.Threading.Tasks;
using Accounts.Api.Models;
using Accounts.Api.Services.AccountService.Requests;
using AlbedoTeam.Sdk.FailFast;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace Accounts.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [OpenApiTag("Accounts", Description = "Albedo's client accounts management")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<PagedAccounts>> List(
            [FromQuery] bool showDeleted,
            [FromQuery] int? page,
            [FromQuery] int? pageSize)
        {
            var response = await _mediator.Send(new List
            {
                ShowDeleted = showDeleted,
                Page = page ?? 1,
                PageSize = pageSize ?? 1
            });

            return response.HasError
                ? HandleError(response)
                : Ok(response.Data);
        }

        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<Account>> Get(string id, [FromQuery] bool showDeleted)
        {
            var response = await _mediator.Send(new Get {Id = id, ShowDeleted = showDeleted});
            return response.HasError
                ? HandleError(response)
                : Ok(response.Data);
        }
        
        [HttpPost]
        public async Task<ActionResult<Account>> Post(Create request)
        {
            var response = await _mediator.Send(request);
            return response.HasError
                ? HandleError(response)
                : CreatedAtRoute(nameof(Get), new {id = response.Data.Id}, response.Data);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Update request)
        {
            if (id != request.Id)
                return BadRequest();
        
            var response = await _mediator.Send(request);
            return response.HasError
                ? HandleError(response)
                : NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _mediator.Send(new Delete {Id = id});
            return response.HasError
                ? HandleError(response)
                : NoContent();
        }

        private ActionResult HandleError<T>(Result<T> response)
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