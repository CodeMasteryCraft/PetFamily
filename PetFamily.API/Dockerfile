FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PetFamily.API/PetFamily.API.csproj", "PetFamily.API/"]
COPY ["PetFamily.Infrastructure/PetFamily.Infrastructure.csproj", "PetFamily.Infrastructure/"]
COPY ["PetFamily.Application/PetFamily.Application.csproj", "PetFamily.Application/"]
COPY ["PetFamily.Domain/PetFamily.Domain.csproj", "PetFamily.Domain/"]
RUN dotnet restore "PetFamily.API/PetFamily.API.csproj"
COPY . .
WORKDIR "/src/PetFamily.API"
RUN dotnet build "PetFamily.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PetFamily.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PetFamily.API.dll"]
