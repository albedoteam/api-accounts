using System.Collections.Generic;
using System.Threading.Tasks;
using Accounts.Contracts.Requests;
using Accounts.Contracts.Responses;
using AlbedoTeam.Accounts.Api.Mappers.Abstractions;
using AlbedoTeam.Accounts.Api.Models;
using AlbedoTeam.Accounts.Api.Services.AccountService.Requests;
using AlbedoTeam.Sdk.FailFast.Abstractions;
using MassTransit;

namespace AlbedoTeam.Accounts.Api.Services.AccountService.Handlers
{
    public class ListHandler : QueryHandler<ListAccounts, PagedAccounts>
    {
        private readonly IRequestClient<ListAccountsRequest> _client;
        private readonly IAccountMapper _mapper;

        public ListHandler(IRequestClient<ListAccountsRequest> client, IAccountMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        protected override async Task<PagedAccounts> Handle(ListAccounts request)
        {
            var (itemsResponse, notFoundResponse) =
                await _client.GetResponse<ListAccountsResponse, AccountNotFound>(
                    _mapper.MapRequestToBroker(request));

            if (itemsResponse.IsCompletedSuccessfully)
            {
                var response = (await itemsResponse).Message;
                return new PagedAccounts
                {
                    Page = response.Page,
                    PageSize = response.PageSize,
                    TotalPages = response.TotalPages,
                    RecordsInPage = response.RecordsInPage,
                    Items = _mapper.MapResponseToModel(response.Items)
                };
            }

            await notFoundResponse;
            return null; // returning null the Response.NotFound will be true
        }
    }
}