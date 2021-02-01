﻿using Accounts.Api.Models.V2;
using AlbedoTeam.Accounts.Contracts.Requests;
using AlbedoTeam.Sdk.FailFast;
using MediatR;

namespace Accounts.Api.Services.AccountService.Requests.V2
{
    public class Update : IRequest<Result<Account>>, UpdateAccount
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IdentificationNumber { get; set; }
        public bool Enabled { get; set; }
    }
}