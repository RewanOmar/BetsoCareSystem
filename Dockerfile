FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# «‰”Œ ﬂ· Õ«Ã…
COPY . .

# restore „‰ solution
RUN dotnet restore BetsoCareSystem.sln

# publish API
RUN dotnet publish BetsoCare.APIS.csproj -c Release -o out

# runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "BetsoCare.APIS.dll"]