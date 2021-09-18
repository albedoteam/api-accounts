﻿namespace Accounts.Api.Controllers
{
    using System.Threading.Tasks;
    using AlbedoTeam.Sdk.FailFast;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using NSwag.Annotations;
    using Services.AccountService.Requests;

    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [OpenApiTag("Accounts", Description = "Albedo's client accounts management")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<Paged<Account>>> List([FromQuery] List request)
        {
            var response = await _mediator.Send(request);

            return response.HasError
                ? HandleError(response)
                : Ok(response.Data);
        }

        [HttpGet("{id:regex(^[[0-9a-fA-F]]{{24}}$)}", Name = "Get")]
        public async Task<ActionResult<Account>> Get(
            string id,
            [FromQuery] bool showDeleted,
            [FromHeader(Name = CustomHeaders.NoCache)]
            bool noCache)
        {
            var response = await _mediator.Send(new Get {NoCache = noCache, Id = id, ShowDeleted = showDeleted});
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

        [HttpPut("{id:regex(^[[0-9a-fA-F]]{{24}}$)}")]
        public async Task<IActionResult> Put(string id, Update request)
        {
            if (id != request.Id)
                return BadRequest();

            var response = await _mediator.Send(request);
            return response.HasError
                ? HandleError(response)
                : NoContent();
        }

        // [Authorize(Roles = "albedo-admins")]
        [HttpDelete("{id:regex(^[[0-9a-fA-F]]{{24}}$)}")]
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