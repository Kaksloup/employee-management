FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SGE.API/SGE.API.csproj", "SGE.API/"]
COPY ["SGE.Application/SGE.Application.csproj", "SGE.Application/"]
COPY ["SGE.Core/SGE.Core.csproj", "SGE.Core/"]
COPY ["SGE.Infrastructure/SGE.Infrastructure.csproj", "SGE.Infrastructure/"]
RUN dotnet restore "SGE.API/SGE.API.csproj"
COPY . .
WORKDIR "/src/SGE.API"
RUN dotnet build "./SGE.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SGE.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SGE.API.dll"]
