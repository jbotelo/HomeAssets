#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HomeAssets/HomeAssets.csproj", "HomeAssets/"]
COPY ["HomeAssets.Domain/HomeAssets.Domain.csproj", "HomeAssets.Domain/"]
COPY ["HomeAssets.Infraestructure/HomeAssets.Infraestructure.csproj", "HomeAssets.Infraestructure/"]
RUN dotnet restore "HomeAssets/HomeAssets.csproj"
COPY . .
WORKDIR "/src/HomeAssets"
RUN dotnet build "HomeAssets.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HomeAssets.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HomeAssets.dll"]