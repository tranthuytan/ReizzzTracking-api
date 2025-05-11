FROM mcr.microsoft.com/dotnet/sdk:8.0@sha256:8ab06772f296ed5f541350334f15d9e2ce84ad4b3ce70c90f2e43db2752c30f6 AS base
USER app
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0@sha256:8ab06772f296ed5f541350334f15d9e2ce84ad4b3ce70c90f2e43db2752c30f6 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ReizzzTracking/ReizzzTracking.csproj","ReizzzTracking/"]
RUN dotnet restore "./ReizzzTracking/ReizzzTracking.csproj"
COPY . .
WORKDIR "/src/ReizzzTracking"
RUN dotnet build "./ReizzzTracking.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ReizzzTracking.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet","ReizzzTracking.dll"]