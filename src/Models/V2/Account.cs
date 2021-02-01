using System;

namespace Accounts.Api.Models.V2
{
    public class Account
    {
        public string Id { get; set; }
        public string AccountName { get; set; }
        public string Description { get; set; }
        public string IdentificationNumber { get; set; }
        public bool Enabled { get; set; }
    }
}