namespace Accounts.Api.Mappers
{
    using Abstractions;
    using AccountGrpc;
    using AutoMapper;
    using Models;
    using Services.AccountService.Requests;
    using Sorting = Models.Sorting;

    public class AccountMapper : IAccountMapper
    {
        private readonly IMapper _mapper;

        public AccountMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                // requests
                cfg.CreateMap<Create, CreateRequest>(MemberList.Destination);
                cfg.CreateMap<Delete, DeleteRequest>(MemberList.Destination);
                cfg.CreateMap<Update, UpdateRequest>(MemberList.Destination);
                cfg.CreateMap<Get, GetRequest>(MemberList.Destination);

                cfg.CreateMap<List, ListRequest>(MemberList.Destination)
                    .ForMember(lr => lr.FilterBy, opt => opt.MapFrom(l => l.FilterBy ?? ""))
                    .ForMember(lr => lr.OrderBy, opt => opt.MapFrom(l => l.OrderBy ?? ""));

                // responses
                cfg.CreateMap<AccountResponse, Account>(MemberList.Destination)
                    .ForMember(a => a.CreatedAt, opt => opt.MapFrom(ar => ar.CreatedAt.ToDateTime()));

                cfg.CreateMap<ListAccountsResponse, Paged<Account>>(MemberList.Destination);

                // enums
                cfg.CreateMap<Sorting, AccountGrpc.Sorting>().ReverseMap();
            });

            _mapper = config.CreateMapper();
        }

        public GetRequest MapRequest(Get request)
        {
            return _mapper.Map<Get, GetRequest>(request);
        }

        public CreateRequest MapRequest(Create request)
        {
            return _mapper.Map<Create, CreateRequest>(request);
        }

        public DeleteRequest MapRequest(Delete request)
        {
            return _mapper.Map<Delete, DeleteRequest>(request);
        }

        public UpdateRequest MapRequest(Update request)
        {
            return _mapper.Map<Update, UpdateRequest>(request);
        }

        public ListRequest MapRequest(List request)
        {
            return _mapper.Map<List, ListRequest>(request);
        }

        public Account MapResponse(AccountResponse response)
        {
            return _mapper.Map<AccountResponse, Account>(response);
        }

        public Paged<Account> MapResponse(ListAccountsResponse response)
        {
            return _mapper.Map<ListAccountsResponse, Paged<Account>>(response);
        }
    }
}