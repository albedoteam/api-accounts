using System.Collections.Generic;
using System.Threading.Tasks;
using Accounts.Api.Models.V2;
using Accounts.Api.Services.AccountService.Requests.V2;
using AlbedoTeam.Accounts.Contracts.Common;
using AlbedoTeam.Sdk.FailFast;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Account = Accounts.Api.Models.V2.Account;

namespace Accounts.Api.Controllers.V2
{
    [ApiController]
    // [Route("api/[controller]")]
    [OpenApiTag("Accounts", Description = "Albedo's client accounts management")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("2")]
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
            [FromQuery] int? pageSize,
            [FromQuery] string name,
            [FromQuery] string description,
            [FromQuery] string identificationNumber,
            [FromQuery] OrderByField orderBy,
            [FromQuery] Sorting sortBy)
        {
            var filters = new Dictionary<FilterByField, string>();

            if (!string.IsNullOrWhiteSpace(name)) filters.Add(FilterByField.Name, name);

            if (!string.IsNullOrWhiteSpace(description)) filters.Add(FilterByField.Description, description);

            if (!string.IsNullOrWhiteSpace(identificationNumber))
                filters.Add(FilterByField.IdentificationNumber, identificationNumber);

            var response = await _mediator.Send(new List
            {
                ShowDeleted = showDeleted,
                Page = page ?? 1,
                PageSize = pageSize ?? 1,
                FilterBy = filters,
                OrderBy = orderBy,
                Sorting = sortBy
            });

            return response.HasError
                ? HandleError(response)
                : Ok(response.Data);
        }

        
        [HttpGet("{id:regex(^[[0-9a-fA-F]]{{24}}$)}", Name = "Get")]
        public async Task<ActionResult<Account>> Get(string id)
        {
            var response = await _mediator.Send(new Get {Id = id, ShowDeleted = false});
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