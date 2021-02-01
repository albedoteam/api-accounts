using System.Threading.Tasks;
using Accounts.Api.Extensions;
using Accounts.Api.Mappers.V2;
using Accounts.Api.Models.V2;
using Accounts.Api.Services.AccountService.Requests.V2;
using AlbedoTeam.Accounts.Contracts.Events;
using AlbedoTeam.Accounts.Contracts.Requests;
using AlbedoTeam.Accounts.Contracts.Responses;
using AlbedoTeam.Sdk.FailFast;
using AlbedoTeam.Sdk.FailFast.Abstractions;
using MassTransit;

namespace Accounts.Api.Services.AccountService.Handlers.V2
{
    public class UpdateHandler : CommandHandler<Update, Account>
    {
        private readonly IRequestClient<UpdateAccount> _client;
        private readonly IAccountMapper _mapper;

        public UpdateHandler(IRequestClient<UpdateAccount> client, IAccountMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        protected override async Task<Result<Account>> Handle(Update request)
        {
            var (successResponse, errorResponse) =
                await _client.GetResponse<AccountUpdated, ErrorResponse>(_mapper.MapRequestToBroker(request));

            if (errorResponse.IsCompletedSuccessfully)
                return await errorResponse.Parse<Account>();

            var accountUpdated = (await successResponse).Message;
            return new Result<Account>(_mapper.MapResponseToModel(accountUpdated));
        }
    }
}