using System.Collections.Generic;

namespace AlbedoTeam.Accounts.Api.Models
{
    public class PagedAccounts
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int RecordsInPage { get; set; }
        public int TotalPages { get; set; }
        public List<Account> Items { get; set; }
    }
}