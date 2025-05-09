FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AssistClub.API/AssistClub.API.csproj", "AssistClub.API/"]
COPY ["AssistClub.Application/AssistClub.Application.csproj", "AssistClub.Application/"]
COPY ["AssistClub.Domain/AssistClub.Domain.csproj", "AssistClub.Domain/"]
COPY ["AssistClub.Infrastructure/AssistClub.Infrastructure.csproj", "AssistClub.Infrastructure/"]
RUN dotnet restore "AssistClub.API/AssistClub.API.csproj"
COPY . .
WORKDIR "/src/AssistClub.API"
RUN dotnet build "AssistClub.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AssistClub.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AssistClub.API.dll"]
