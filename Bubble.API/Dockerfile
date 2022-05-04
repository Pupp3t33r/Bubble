#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Bubble.API/Bubble.API.csproj", "Bubble.API/"]
COPY ["Bubble.Service/Bubble.CQS.csproj", "Bubble.Service/"]
COPY ["Bubble/Bubble.Data.csproj", "Bubble/"]
COPY ["Bubble.Shared/Bubble.Shared.csproj", "Bubble.Shared/"]
COPY ["Bubble.DataScraping/Bubble.DataScraping.fsproj", "Bubble.DataScraping/"]
COPY ["Bubble.APIServices/Bubble.APIServices.csproj", "Bubble.APIServices/"]
RUN dotnet restore "Bubble.API/Bubble.API.csproj"
COPY . .
WORKDIR "/src/Bubble.API"
RUN dotnet build "Bubble.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bubble.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bubble.API.dll"]