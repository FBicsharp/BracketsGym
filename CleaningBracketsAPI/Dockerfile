FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CleaningBracketsAPI/CleaningBracketsAPI.csproj", "CleaningBracketsAPI/"]
RUN dotnet restore "CleaningBracketsAPI/CleaningBracketsAPI.csproj"
COPY . .
WORKDIR "/src/CleaningBracketsAPI"
RUN dotnet build "CleaningBracketsAPI.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "CleaningBracketsAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

COPY ["CleaningBracketsAPI.Test/CleaningBracketsAPI.Test.csproj", "CleaningBracketsAPI.Test/"]
RUN dotnet restore "CleaningBracketsAPI.Test/CleaningBracketsAPI.Test.csproj"
COPY . .
WORKDIR "/src/CleaningBracketsAPI.Test"
RUN dotnet build "CleaningBracketsAPI.Test.csproj" -c Release -o /app/testbuild

# Esegui i test
RUN dotnet test "CleaningBracketsAPI.Test/CleaningBracketsAPI.Test.csproj" --logger:trx

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CleaningBracketsAPI.dll"]