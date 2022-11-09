#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CarLocadora.Front/CarLocadora.Front.csproj", "CarLocadora.Front/"]
COPY ["CarLocadora.Comum/CarLocadora.Comum.csproj", "CarLocadora.Comum/"]
COPY ["CarLocadora.Modelo/CarLocadora.Modelo.csproj", "CarLocadora.Modelo/"]
RUN dotnet restore "CarLocadora.Front/CarLocadora.Front.csproj"
COPY . .
WORKDIR "/src/CarLocadora.Front"
RUN dotnet build "CarLocadora.Front.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarLocadora.Front.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarLocadora.Front.dll" , "--environment=Development"]