FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . .

WORKDIR /app

RUN ls

RUN dotnet restore BetsoCareSystem.sln
RUN dotnet publish BetsoCare.APIS.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "BetsoCare.APIS.dll"]