namespace Accounts.Api.Services.AccountService.Handlers
{
    using System.Threading.Tasks;
    using AccountGrpc;
    using AlbedoTeam.Sdk.FailFast;
    using AlbedoTeam.Sdk.FailFast.Abstractions;
    using Extensions;
    using Grpc.Core;
    using Mappers.Abstractions;
    using Models;
    using Requests;

    public class DeleteHandler : CommandHandler<Delete, Account>
    {
        private readonly Accounts.AccountsClient _client;
        private readonly IAccountMapper _mapper;

        public DeleteHandler(Accounts.AccountsClient client, IAccountMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        protected override async Task<Result<Account>> Handle(Delete request)
        {
            try
            {
                var response = await _client.DeleteAsync(_mapper.MapRequest(request));
                return new Result<Account>(_mapper.MapResponse(response));
            }
            catch (RpcException e)
            {
                return e.Parse<Account>();
            }
        }
    }
}