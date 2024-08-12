# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

ARG src="Entertainment web app"

# copy csproj and restore as distinct layers
COPY *.sln .
COPY ${src}/*.csproj ./${src}/
COPY ${src}.Tests/*.csproj ./${src}.Tests/
RUN dotnet restore

# copy everything else and build app
COPY ${src}/. ./${src}/
WORKDIR /source/${src}
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "Entertainment_web_app.dll"]
