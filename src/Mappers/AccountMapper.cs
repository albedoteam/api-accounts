namespace Accounts.Api.Mappers
{
    using System.Collections.Generic;
    using Abstractions;
    using AlbedoTeam.Accounts.Contracts.Events;
    using AlbedoTeam.Accounts.Contracts.Requests;
    using AlbedoTeam.Accounts.Contracts.Responses;
    using AutoMapper;
    using Models;
    using Services.AccountService.Requests;

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