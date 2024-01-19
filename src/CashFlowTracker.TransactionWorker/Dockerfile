FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copiar csproj e restaurar as dependências
COPY ["src/CashFlowTracker.TransactionWorker/CashFlowTracker.TransactionWorker.csproj", "./"]
RUN dotnet restore "./CashFlowTracker.TransactionWorker.csproj"

# Copiar o resto dos arquivos e construir
COPY src/CashFlowTracker.TransactionWorker/ ./
RUN dotnet publish -c Release -o out

# Build da imagem de runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "CashFlowTracker.TransactionWorker.dll"]
