#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CarLocadora.API/CarLocadora.API.csproj", "CarLocadora.API/"]
COPY ["CarLocadora.Negocio/CarLocadora.Negocio.csproj", "CarLocadora.Negocio/"]
COPY ["CarLocadora.Infra/CarLocadora.Infra.csproj", "CarLocadora.Infra/"]
COPY ["CarLocadora.Modelo/CarLocadora.Modelo.csproj", "CarLocadora.Modelo/"]
RUN dotnet restore "CarLocadora.API/CarLocadora.API.csproj"
COPY . .
WORKDIR "/src/CarLocadora.API"
RUN dotnet build "CarLocadora.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarLocadora.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarLocadora.API.dll" , "--environment=Development"]