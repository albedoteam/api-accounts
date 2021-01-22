using System.Collections.Generic;
using Accounts.Api.Models;
using AlbedoTeam.Accounts.Contracts.Common;
using AlbedoTeam.Accounts.Contracts.Requests;
using AlbedoTeam.Sdk.FailFast;
using MediatR;

namespace Accounts.Api.Services.AccountService.Requests
{
    public class List : IRequest<Result<PagedAccounts>>, ListAccounts
    {
        public bool ShowDeleted { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public Dictionary<FilterByField, string> FilterBy { get; set; }
        public OrderByField OrderBy { get; set; }
        public Sorting Sorting { get; set; }
    }
}