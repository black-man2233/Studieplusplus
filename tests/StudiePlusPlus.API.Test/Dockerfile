FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["tests/StudiePlusPlus.API.Test/StudiePlusPlus.API.Test.csproj", "tests/StudiePlusPlus.API.Test/"]
RUN dotnet restore "tests/StudiePlusPlus.API.Test/StudiePlusPlus.API.Test.csproj"
COPY . .
WORKDIR "/src/tests/StudiePlusPlus.API.Test"
RUN dotnet build "./StudiePlusPlus.API.Test.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./StudiePlusPlus.API.Test.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StudiePlusPlus.API.Test.dll"]
