#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["SimpleMailService - SMS/SimpleMailService - SMS.csproj", "SimpleMailService - SMS/"]
RUN dotnet restore "SimpleMailService - SMS/SimpleMailService - SMS.csproj"
COPY . .
WORKDIR "/src/SimpleMailService - SMS"
RUN dotnet build "SimpleMailService - SMS.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SimpleMailService - SMS.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleMailService - SMS.dll"]