FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0  as build-env
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

#COPY ["CleaningBracketsAPI.Test/CleaningBracketsAPI.Test.csproj", "CleaningBracketsAPI.Test/"]
#RUN dotnet restore "CleaningBracketsAPI.Test/CleaningBracketsAPI.Test.csproj"
#COPY . .
#WORKDIR "/src/CleaningBracketsAPI.Test"
#RUN dotnet build "CleaningBracketsAPI.Test.csproj" -c Release -o /app/testbuild
#
## Esegui i test
#RUN dotnet test "CleaningBracketsAPI.Test/CleaningBracketsAPI.Test.csproj" --logger:trx

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CleaningBracketsAPI.dll"]