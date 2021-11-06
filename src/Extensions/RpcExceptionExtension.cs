namespace Accounts.Api.Extensions
{
    using AlbedoTeam.Sdk.FailFast;
    using Grpc.Core;

    public static class RpcExceptionExtension
    {
        public static Result<T> Parse<T>(this RpcException exception)
        {
            return exception.StatusCode switch
            {
                StatusCode.InvalidArgument => new Result<T>(FailureReason.BadRequest, exception.Message),
                StatusCode.NotFound => new Result<T>(FailureReason.NotFound, exception.Message),
                StatusCode.AlreadyExists => new Result<T>(FailureReason.Conflict, exception.Message),
                _ => new Result<T>(FailureReason.InternalServerError, exception.Message)
            };
        }
    }
}