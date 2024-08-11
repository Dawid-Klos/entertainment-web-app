FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY Entertainment\ web\ app/*.csproj .
RUN dotnet restore

# copy everything else and build app
COPY Entertainment\ web\ app/. .
RUN dotnet publish --no-restore -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
EXPOSE 8080
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["./aspnetapp"]
