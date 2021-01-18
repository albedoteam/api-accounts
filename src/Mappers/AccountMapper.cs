using System.Collections.Generic;
using Accounts.Api.Mappers.Abstractions;
using Accounts.Api.Models;
using Accounts.Api.Services.AccountService.Requests;
using Accounts.Events;
using Accounts.Requests;
using Accounts.Responses;
using AutoMapper;

namespace Accounts.Api.Mappers
{
    public class AccountMapper : IAccountMapper
    {
        private readonly IMapper _mapper;

        public AccountMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                // Broker to Model
                cfg.CreateMap<Account, AccountResponse>().ReverseMap();

                // MediatR to Broker
                cfg.CreateMap<Create, CreateAccount>().ReverseMap();
                cfg.CreateMap<Delete, DeleteAccount>().ReverseMap();
                cfg.CreateMap<Update, UpdateAccount>().ReverseMap();
                cfg.CreateMap<Get, GetAccount>().ReverseMap();
                cfg.CreateMap<List, ListAccounts>().ReverseMap();
                cfg.CreateMap<Account, AccountDeleted>().ReverseMap();
                cfg.CreateMap<Account, AccountUpdated>().ReverseMap();
            });

            _mapper = config.CreateMapper();
        }

        public Account MapResponseToModel(AccountDeleted response)
        {
            return _mapper.Map<AccountDeleted, Account>(response);
        }

        public Account MapResponseToModel(AccountUpdated response)
        {
            return _mapper.Map<AccountUpdated, Account>(response);
        }

        public Account MapResponseToModel(AccountResponse response)
        {
            return _mapper.Map<AccountResponse, Account>(response);
        }

        public List<Account> MapResponseToModel(List<AccountResponse> response)
        {
            return _mapper.Map<List<AccountResponse>, List<Account>>(response);
        }

        public CreateAccount MapRequestToBroker(Create request)
        {
            return _mapper.Map<Create, CreateAccount>(request);
        }

        public DeleteAccount MapRequestToBroker(Delete request)
        {
            return _mapper.Map<Delete, DeleteAccount>(request);
        }

        public UpdateAccount MapRequestToBroker(Update request)
        {
            return _mapper.Map<Update, UpdateAccount>(request);
        }

        public GetAccount MapRequestToBroker(Get request)
        {
            return _mapper.Map<Get, GetAccount>(request);
        }

        public ListAccounts MapRequestToBroker(List request)
        {
            return _mapper.Map<List, ListAccounts>(request);
        }
    }
}