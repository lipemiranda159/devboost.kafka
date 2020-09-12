FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine3.11 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine3.11 AS build
WORKDIR /src
COPY ["ApiConsumer/ApiConsumer.csproj", "ApiConsumer/"]
COPY ["Utils/Utils.csproj", "Utils/"]

RUN dotnet restore "ApiConsumer/ApiConsumer.csproj"
COPY . .
WORKDIR "/src/ApiConsumer"
RUN dotnet build "ApiConsumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiConsumer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiConsumer.dll"]