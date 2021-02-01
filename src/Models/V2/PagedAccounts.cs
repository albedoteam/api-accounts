using System.Collections.Generic;
using AlbedoTeam.Accounts.Contracts.Common;

namespace Accounts.Api.Models.V2
{
    public class PagedAccounts
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int RecordsInPage { get; set; }
        public int TotalPages { get; set; }
        public Dictionary<FilterByField, string> FilteredBy { get; set; }
        public OrderByField OrderedBy { get; set; }
        public Sorting Sorting { get; set; }
        public List<Account> Records { get; set; }
    }
}