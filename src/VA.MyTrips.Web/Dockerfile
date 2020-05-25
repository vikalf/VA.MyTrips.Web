#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/VA.MyTrips.Web/VA.MyTrips.Web.csproj", "src/VA.MyTrips.Web/"]
RUN dotnet restore "src/VA.MyTrips.Web/VA.MyTrips.Web.csproj"
COPY . .
WORKDIR "/src/src/VA.MyTrips.Web"
RUN dotnet build "VA.MyTrips.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VA.MyTrips.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VA.MyTrips.Web.dll"]