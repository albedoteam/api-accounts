using System.Threading.Tasks;
using Accounts.Api.Extensions;
using Accounts.Api.Mappers.Abstractions;
using Accounts.Api.Models;
using Accounts.Api.Services.AccountService.Requests;
using AlbedoTeam.Accounts.Contracts.Requests;
using AlbedoTeam.Accounts.Contracts.Responses;
using AlbedoTeam.Sdk.FailFast;
using AlbedoTeam.Sdk.FailFast.Abstractions;
using MassTransit;

namespace Accounts.Api.Services.AccountService.Handlers
{
    public class ListHandler : QueryHandler<List, PagedAccounts>
    {
        private readonly IRequestClient<ListAccounts> _client;
        private readonly IAccountMapper _mapper;

        public ListHandler(IRequestClient<ListAccounts> client, IAccountMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        protected override async Task<Result<PagedAccounts>> Handle(List request)
        {
            var (successResponse, errorResponse) = await _client.GetResponse<ListAccountsResponse, ErrorResponse>(
                _mapper.MapRequestToBroker(request));

            if (errorResponse.IsCompletedSuccessfully)
                return await errorResponse.Parse<PagedAccounts>();

            var accounts = (await successResponse).Message;
            var paged = new PagedAccounts
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

            return new Result<PagedAccounts>(paged);
        }
    }
}