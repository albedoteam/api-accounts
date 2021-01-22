using System;
using System.Threading.Tasks;
using AlbedoTeam.Accounts.Contracts.Responses;
using AlbedoTeam.Sdk.FailFast;
using MassTransit;

namespace Accounts.Api.Extensions
{
    public static class ErrorResponseExtension
    {
        public static async Task<Result<T>> Parse<T>(this Task<Response<ErrorResponse>> errorResponse)
            where T : class
        {
            var error = await errorResponse;
            var message = error.Message.ErrorMessage;
        
            return error.Message.ErrorType switch
            {
                ErrorType.AlreadyExists => new Result<T>(FailureReason.Conflict, message),
                ErrorType.InvalidOperation => new Result<T>(FailureReason.BadRequest, message),
                ErrorType.InternalServerError => new Result<T>(FailureReason.InternalServerError, message),
                ErrorType.NotFound => new Result<T>(FailureReason.NotFound, message),
                _ => throw new Exception(error.Message.ErrorMessage)
            };
        }
    }
}