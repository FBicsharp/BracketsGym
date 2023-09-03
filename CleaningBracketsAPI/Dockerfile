FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /App
EXPOSE 33500
EXPOSE 33501

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet","CleaningBracketsAPI.dll"]

# docker build -t fbicsharp/cleaningbracketsapi:V0.1 .
# docker run -p 5000:80 fbicsharp/cleaningbracketsapi:V0.1
# docker push fbicsharp/cleaningbracketsapi:V0.1
