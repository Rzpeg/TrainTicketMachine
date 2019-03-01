FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY . .
RUN dotnet restore "TrainTicketMachine.sln"
RUN dotnet test
RUN dotnet build "TrainTicketMachine.Api/TrainTicketMachine.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TrainTicketMachine.Api/TrainTicketMachine.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TrainTicketMachine.Api.dll"]