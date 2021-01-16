using System.Collections.Generic;
using Accounts.Api._Broker.Events;
using Accounts.Api.Mappers.Abstractions;
using Accounts.Api.Models;
using Accounts.Api.Services.AccountService.Requests;
using Accounts.Contracts.Requests;
using Accounts.Contracts.Responses;
using AutoMapper;

namespace src.Mappers
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
                cfg.CreateMap<CreateAccount, CreateAccountRequest>().ReverseMap();
                cfg.CreateMap<DeleteAccount, DeleteAccountRequest>().ReverseMap();
                cfg.CreateMap<UpdateAccount, UpdateAccountRequest>().ReverseMap();
                cfg.CreateMap<GetAccount, GetAccountRequest>().ReverseMap();
                cfg.CreateMap<ListAccounts, ListAccountsRequest>().ReverseMap();
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

        public CreateAccountRequest MapRequestToBroker(CreateAccount request)
        {
            return _mapper.Map<CreateAccount, CreateAccountRequest>(request);
        }

        public DeleteAccountRequest MapRequestToBroker(DeleteAccount request)
        {
            return _mapper.Map<DeleteAccount, DeleteAccountRequest>(request);
        }

        public UpdateAccountRequest MapRequestToBroker(UpdateAccount request)
        {
            return _mapper.Map<UpdateAccount, UpdateAccountRequest>(request);
        }

        public GetAccountRequest MapRequestToBroker(GetAccount request)
        {
            return _mapper.Map<GetAccount, GetAccountRequest>(request);
        }

        public ListAccountsRequest MapRequestToBroker(ListAccounts request)
        {
            return _mapper.Map<ListAccounts, ListAccountsRequest>(request);
        }
    }
}