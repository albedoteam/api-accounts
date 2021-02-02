using System.Collections.Generic;
using System.Threading.Tasks;
using Accounts.Api.Mappers.Abstractions;
using Accounts.Api.Models;
using Accounts.Api.Services.AccountService.Requests;
using AlbedoTeam.Accounts.Contracts.Requests;
using AlbedoTeam.Accounts.Contracts.Responses;
using HotChocolate.Types;
using MassTransit;

namespace Accounts.Api.GraphQL.AccountOperations
{
    public class ListAccountQuery
    {
        private readonly IRequestClient<GetAccount> _client;
        private readonly IAccountMapper _mapper;

        public ListAccountQuery(IRequestClient<GetAccount> client, IAccountMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }
        
        [UsePaging]
        [UseFiltering]
        public async Task<List<Account>> GetAccounts(List request)
        {
            var (successResponse, errorResponse) = await _client.GetResponse<ListAccountsResponse, ErrorResponse>(
                _mapper.MapRequestToBroker(request));

            if (errorResponse.IsCompletedSuccessfully)
                return null;

            var accounts = (await successResponse).Message;
            // var paged = new PagedAccounts
            // {
            //     Page = accounts.Page,
            //     PageSize = accounts.PageSize,
            //     TotalPages = accounts.TotalPages,
            //     RecordsInPage = accounts.RecordsInPage,
            //     Items = _mapper.MapResponseToModel(accounts.Items),
            //     FilterBy = accounts.FilterBy,
            //     OrderBy = accounts.OrderBy,
            //     Sorting = accounts.Sorting
            // };

            // return new Result<PagedAccounts>(paged);

            return _mapper.MapResponseToModel(accounts.Items);
        }
    }
}