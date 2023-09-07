FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-env
WORKDIR /src
COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet build -c Release 
RUN dotnet test 


# todo runnng tests that is in project under path ../CleaningBracketsAPI.Test 



# docker build -t fbicsharp/cleaningbracketsapitest:V0.1 .
# docker run -p 5000:80 fbicsharp/cleaningbracketsapi:V0.1
# docker push fbicsharp/cleaningbracketsapi:V0.1
