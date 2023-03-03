#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CSharp_Assignment_3_MovieApi/CSharp_Assignment_3_MovieApi.csproj", "CSharp_Assignment_3_MovieApi/"]
RUN dotnet restore "CSharp_Assignment_3_MovieApi/CSharp_Assignment_3_MovieApi.csproj"
COPY . .
WORKDIR "/src/CSharp_Assignment_3_MovieApi"
RUN dotnet build "CSharp_Assignment_3_MovieApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CSharp_Assignment_3_MovieApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CSharp_Assignment_3_MovieApi.dll"]