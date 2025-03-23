# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy solution and projects
COPY TaxReturnAPI.sln ./
COPY BusinessLogic/*.csproj BusinessLogic/
COPY BusinessLogic/Tax.Entities/*.csproj BusinessLogic/Tax.Entities/
COPY BusinessLogic/Tax.Services/*.csproj BusinessLogic/Tax.Services/
COPY BusinessLogic/Tax.Services.Implementations/*.csproj BusinessLogic/Tax.Services.Implementations/
COPY BusinessLogic/Tax.Services.Implementations.IoC/*.csproj BusinessLogic/Tax.Services.Implementations.IoC/
COPY BusinessLogic/Tax.Services.Implementations.UnitTests/*.csproj BusinessLogic/Tax.Services.Implementations.UnitTests/
COPY WebAPI/Tax.AcceptanceTests/*.csproj WebAPI/Tax.AcceptanceTests/
COPY WebAPI/Tax.DataTransferring/*.csproj WebAPI/Tax.DataTransferring/
COPY WebAPI/Tax.DataTransferring.Entities.Mapping/*.csproj WebAPI/Tax.DataTransferring.Entities.Mapping/
COPY WebAPI/TaxReturnAPI/*.csproj WebAPI/TaxReturnAPI/

# Restore dependencies
RUN dotnet restore

# Copy the entire project
COPY . .

# Build and publish
WORKDIR /app/WebAPI/TaxReturnAPI
RUN dotnet publish -c Release -o /out

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out ./

# Expose ports
EXPOSE 5164
EXPOSE 7032

# Start the application
ENTRYPOINT ["dotnet", "TaxReturnAPI.dll"]