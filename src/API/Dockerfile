#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/API/API.csproj", "src/API/"]
COPY ["src/Application/Application.csproj", "src/Application/"]
COPY ["src/commons/CQRS/CQRS.csproj", "src/commons/CQRS/"]
COPY ["src/commons/Message/Message.csproj", "src/commons/Message/"]
COPY ["src/Domain/Domain.csproj", "src/Domain/"]
COPY ["src/Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]
COPY ["src/commons/CommonTypes/CommonTypes.csproj", "src/commons/CommonTypes/"]
COPY ["src/commons/Mapper/Mapper.csproj", "src/commons/Mapper/"]
RUN dotnet restore "src/API/API.csproj"
COPY . .
WORKDIR "/src/src/API"
RUN dotnet build "API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]