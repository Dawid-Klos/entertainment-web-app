FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source
ARG src="Entertainment web app"

# copy csproj and restore as distinct layers
COPY ${src}/*.csproj .
RUN dotnet restore

# copy everything else and build app
COPY ${src}/. .
RUN dotnet publish --no-restore -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
EXPOSE 8080
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["./Entertainment web app"]
