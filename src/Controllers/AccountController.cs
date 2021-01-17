using System.Threading.Tasks;
using Accounts.Api.Models;
using Accounts.Api.Services.AccountService.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace Accounts.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [OpenApiTag("Demiurge - Accounts", Description = "Accounts management")]
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
            var response = await _mediator.Send(new ListAccounts
            {
                ShowDeleted = showDeleted,
                Page = page ?? 1,
                PageSize = pageSize ?? 1
            });

            if (response.HasError)
                return BadRequest(response.Errors);

            if (response.NotFound)
                return NotFound();

            return Ok(response.Result);
        }

        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<Account>> Get(string id, [FromQuery] bool showDeleted)
        {
            var response = await _mediator.Send(new GetAccount {Id = id, ShowDeleted = showDeleted});

            if (response.HasError)
                return BadRequest(response.Errors);

            if (response.NotFound)
                return NotFound();

            return Ok(response.Result);
        }

        [HttpPost]
        public async Task<ActionResult<Account>> Post(CreateAccount request)
        {
            var response = await _mediator.Send(request);

            if (response.HasError)
                return BadRequest(response.Errors);

            if (response.Conflict)
                return Conflict();

            return CreatedAtRoute(nameof(Get), new {id = response.Result.Id}, response.Result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, UpdateAccount request)
        {
            if (id != request.Id)
                return BadRequest();

            var response = await _mediator.Send(request);

            if (response.HasError)
                return BadRequest(response.Errors);

            if (response.NotFound)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _mediator.Send(new DeleteAccount {Id = id});

            if (response.HasError)
                return BadRequest(response.Errors);

            if (response.NotFound)
                return NotFound();

            return NoContent();
        }
    }
}