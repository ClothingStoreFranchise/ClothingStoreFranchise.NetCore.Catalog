FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 4551

ARG csproj=ClothingStoreFranchise.NetCore.Catalog.csproj
ARG configuration=Release
ARG feed='--source "https://api.nuget.org/v3/index.json"'

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["${csproj}", "./"]
RUN dotnet restore "./${csproj}" ${feed}
COPY . .
WORKDIR /src/.
RUN dotnet build "${csproj}" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "${csproj}" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClothingStoreFranchise.NetCore.Catalog.dll"]