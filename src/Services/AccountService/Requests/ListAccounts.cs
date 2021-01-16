﻿using AlbedoTeam.Accounts.Api.Models;
using AlbedoTeam.Sdk.FailFast;
using MediatR;

namespace AlbedoTeam.Accounts.Api.Services.AccountService.Requests
{
    public class ListAccounts : IRequest<Response<PagedAccounts>>
    {
        public bool ShowDeleted { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}