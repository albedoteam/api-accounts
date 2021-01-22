using System.Collections.Generic;
using AlbedoTeam.Accounts.Contracts.Common;
using AlbedoTeam.Accounts.Contracts.Requests;

namespace Accounts.Api.Models
{
    public class PagedAccounts
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int RecordsInPage { get; set; }
        public int TotalPages { get; set; }
        public Dictionary<FilterByField, string> FilterBy { get; set; }
        public OrderByField OrderBy { get; set; }
        public Sorting Sorting { get; set; }
        public List<Account> Items { get; set; }
    }
}