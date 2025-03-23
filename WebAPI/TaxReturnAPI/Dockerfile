FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Dependencies
COPY *.sln ./
COPY BusinessLogic/*.csproj BusinessLogic/
COPY BusinessLogic/Tax.Entities/*.csproj BusinessLogic/Tax.Entities/
COPY BusinessLogic/Tax.Services/*.csproj BusinessLogic/Tax.Services/
COPY BusinessLogic/Tax.Services.Implementations/*.csproj BusinessLogic/Tax.Services.Implementations/
COPY BusinessLogic/Tax.Services.Implementations.IoC/*.csproj BusinessLogic/Tax.Services.Implementations.IoC/
COPY BusinessLogic/Tax.Services.Implementations.UnitTests/*.csproj BusinessLogic/Tax.Services.Implementations.UnitTests/

COPY WebApi/*.csproj WebApi/
COPY WebApi/Tax.AcceptanceTests/*.csproj WebApi/Tax.AcceptanceTests/
COPY WebApi/Tax.DataTransferring/*.csproj WebApi/Tax.DataTransferring/
COPY WebApi/Tax.DataTransferring.Entities.Mapping/*.csproj WebApi/Tax.DataTransferring.Entities.Mapping/
COPY WebApi/TaxReturnAPI/*.csproj WebApi/TaxReturnAPI/

RUN dotnet restore

# Copy everything else and build the app
COPY . .
WORKDIR /app/WebApi/TaxReturnAPI
RUN dotnet publish -c Release -o out

# run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/WebApi/TaxReturnAPI/out ./
EXPOSE 5000
EXPOSE 5001
ENTRYPOINT ["dotnet", "TaxReturnAPI.dll"]