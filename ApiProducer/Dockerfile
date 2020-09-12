FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine3.11 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine3.11 AS build
WORKDIR /src
COPY ["ApiProducer/ApiProducer.csproj", "ApiProducer/"]
COPY ["Utils/Utils.csproj", "Utils/"]

RUN dotnet restore "ApiProducer/ApiProducer.csproj"
COPY . .
WORKDIR "/src/ApiProducer"
RUN dotnet build "ApiProducer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiProducer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiProducer.dll"]