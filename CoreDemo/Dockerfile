#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CoreDemo/CoreDemo.csproj", "CoreDemo/"]
COPY ["EntityLayer/EntityLayer.csproj", "EntityLayer/"]
COPY ["BusinessLayer/BusinessLayer.csproj", "BusinessLayer/"]
COPY ["DataAccessLayer/DataAccessLayer.csproj", "DataAccessLayer/"]
RUN dotnet restore "CoreDemo/CoreDemo.csproj"
COPY . .
WORKDIR "/src/CoreDemo"
RUN dotnet build "CoreDemo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CoreDemo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoreDemo.dll"]
