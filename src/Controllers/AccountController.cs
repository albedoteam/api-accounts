using System.Collections.Generic;
using System.Threading.Tasks;
using Accounts.Api.Models;
using Accounts.Api.Services.AccountService.Requests;
using AlbedoTeam.Accounts.Contracts.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace Accounts.Api.Controllers
{
    [Authorize(Roles = "albedo-admins")]
    [ApiVersion("1")]
    [OpenApiTag("Accounts", Description = "Albedo's client accounts management")]
    public class AccountController : AlbedoControllerBase
    {
        public AccountController(IMediator mediator) : base(mediator)
        {
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

            var response = await Mediator.Send(new List
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
        public async Task<ActionResult<Account>> Get(string id, [FromQuery] bool showDeleted)
        {
            var response = await Mediator.Send(new Get {Id = id, ShowDeleted = showDeleted});
            return response.HasError
                ? HandleError(response)
                : Ok(response.Data);
        }

        [HttpPost]
        public async Task<ActionResult<Account>> Post(Create request)
        {
            var response = await Mediator.Send(request);
            return response.HasError
                ? HandleError(response)
                : CreatedAtRoute(nameof(Get), new {id = response.Data.Id}, response.Data);
        }

        [HttpPut("{id:regex(^[[0-9a-fA-F]]{{24}}$)}")]
        public async Task<IActionResult> Put(string id, Update request)
        {
            if (id != request.Id)
                return BadRequest();

            var response = await Mediator.Send(request);
            return response.HasError
                ? HandleError(response)
                : NoContent();
        }

        [HttpDelete("{id:regex(^[[0-9a-fA-F]]{{24}}$)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await Mediator.Send(new Delete {Id = id});
            return response.HasError
                ? HandleError(response)
                : NoContent();
        }
    }
}