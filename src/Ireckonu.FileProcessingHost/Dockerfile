#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Ireckonu.FileProcessingHost/Ireckonu.FileProcessingHost.csproj", "src/Ireckonu.FileProcessingHost/"]
COPY ["src/Ireckonu.Infrastructure/Ireckonu.Infrastructure.csproj", "src/Ireckonu.Infrastructure/"]
COPY ["src/Ireckonu.Application/Ireckonu.Application.csproj", "src/Ireckonu.Application/"]
RUN dotnet restore "src/Ireckonu.FileProcessingHost/Ireckonu.FileProcessingHost.csproj"
COPY . .
WORKDIR "/src/src/Ireckonu.FileProcessingHost"
RUN dotnet build "Ireckonu.FileProcessingHost.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ireckonu.FileProcessingHost.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ireckonu.FileProcessingHost.dll"]
