namespace Accounts.Api.Services.AccountService.Handlers
{
    using System.Threading.Tasks;
    using AlbedoTeam.Accounts.Contracts.Requests;
    using AlbedoTeam.Accounts.Contracts.Responses;
    using AlbedoTeam.Sdk.FailFast;
    using AlbedoTeam.Sdk.FailFast.Abstractions;
    using Extensions;
    using Mappers.Abstractions;
    using MassTransit;
    using Models;
    using Requests;

    public class ListHandler : QueryHandler<List, Paged<Account>>
    {
        private readonly IRequestClient<ListAccounts> _client;
        private readonly IAccountMapper _mapper;

        public ListHandler(IRequestClient<ListAccounts> client, IAccountMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        protected override async Task<Result<Paged<Account>>> Handle(List request)
        {
            var (successResponse, errorResponse) = await _client.GetResponse<ListAccountsResponse, ErrorResponse>(
                _mapper.MapRequestToBroker(request));

            if (errorResponse.IsCompletedSuccessfully)
                return await errorResponse.Parse<Paged<Account>>();

            var accounts = (await successResponse).Message;
            var paged = new Paged<Account>
            {
                Page = accounts.Page,
                PageSize = accounts.PageSize,
                TotalPages = accounts.TotalPages,
                RecordsInPage = accounts.RecordsInPage,
                Items = _mapper.MapResponseToModel(accounts.Items),
                FilterBy = accounts.FilterBy,
                OrderBy = accounts.OrderBy,
                Sorting = accounts.Sorting
            };

            return new Result<Paged<Account>>(paged);
        }
    }
}