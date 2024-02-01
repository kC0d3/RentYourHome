FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 9090

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["RentYourHome.csproj", "."]
RUN dotnet restore "RentYourHome.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "RentYourHome.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RentYourHome.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RentYourHome.dll"]
ENV ASPNETCORE_URLS=http://+:9090/