FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
ARG BUILD_OUTPUT

COPY ${BUILD_OUTPUT}/ app/

WORKDIR /app
ENTRYPOINT ["dotnet", "Accounts.Api.dll"]