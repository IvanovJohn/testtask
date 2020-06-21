#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["PipelinesApp.Api/PipelinesApp.Api.csproj", "PipelinesApp.Api/"]
RUN dotnet restore "PipelinesApp.Api/PipelinesApp.Api.csproj"
COPY . .
RUN dotnet publish "PipelineApp.TaskExample/PipelineApp.TaskExample.csproj" -c Release -o /app/publish
WORKDIR "/src/PipelinesApp.Api"
RUN dotnet build "PipelinesApp.Api.csproj" -c Release -o /app/build

FROM build AS publish

RUN dotnet publish "PipelinesApp.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PipelinesApp.Api.dll"]
