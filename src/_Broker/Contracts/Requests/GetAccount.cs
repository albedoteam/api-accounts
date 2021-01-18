namespace Accounts.Requests
{
    public interface GetAccount
    {
        string Id { get; set; }
        bool ShowDeleted { get; set; }
    }
}